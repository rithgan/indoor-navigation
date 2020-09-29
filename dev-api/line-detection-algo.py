#!/usr/bin/env python
# coding: utf-8

# In[1]:


import cv2
import numpy as np
import json
from pytesseract import *

class extraction:
    save_image=0
    data={'floor':{'number':0,'walls':[],'text_data':[]},'ratio':[]}

    @staticmethod
    def get_image(img_location):
        image = cv2.imread(img_location,0)
        image = np.array(image)
        # print(image.shape)
        scale_x=image.shape[0]/500
        scale_y = image.shape[1]/500
        extraction.data['ratio']=[scale_x,scale_y]
        image =cv2.resize(image,(500,500))
        # print(image.shape)
        background_value=image[0][0]
        for i in range(500):
            for j in range(500):
                image[i][j]=255-image[i][j]
        extraction.save_image=image





    @staticmethod
    def detecttext():
        pytesseract.tesseract_cmd =r'C:\Program Files\Tesseract-OCR\tesseract.exe'
        rgb = cv2.cvtColor(extraction.save_image, cv2.COLOR_BGR2RGB)
        results = pytesseract.image_to_data(rgb, output_type=Output.DICT)
        text_data=[]
        first_pixel=int(extraction.save_image[0][0])
        for i in range(len(results["text"])):

            x = results["left"][i]
            y = results["top"][i]
            w = results["width"][i]
            h = results["height"][i]

            text = results["text"][i]
            conf = int(results["conf"][i])

            if conf > 85 and len(text.split())!=0:

                #print("Confidence: {}".format(conf))
                #print("Text: {}".format(text))

                text = "".join(text).strip()
                if text.isnumeric():
                    continue
                extraction.save_image=cv2.rectangle(extraction.save_image,(x-5, y-5),(x + w+5, y + h+5),first_pixel, -1)
                #cv2.putText(images,text,(x, y - 10),cv2.FONT_HERSHEY_SIMPLEX,1.2, (0, 255, 255), 3)
                text_data.append({'text':text,'x':x+w//2,'y':y+h//2})
        extraction.data['floor']['text_data']=text_data
        
        # cv2.imshow("Image", images)
        # cv2.waitKey(0)
        # cv2.destroyAllWindows()

    @staticmethod
    def detectline(json_location='',max_gap=2,threshold=50):

        points=np.empty((0,2))
        for i in range(extraction.save_image.shape[0]):
            for j in range(extraction.save_image.shape[1]):
                if extraction.save_image[i][j]<threshold:
                    points=np.concatenate([points,np.array([[i,j]])],axis=0)

        lines=[]
        mcondition=True
        while True:
            i=0
            # print(i)
            valid=np.zeros((1,4))
            mx_in_line=0
            mindexes=np.empty((0,),dtype=int)
            for theta in range(180):
                gap=0
                x1=points[i][1]
                y1=points[i][0]
                x2=points[i][1]
                y2=points[i][0]
                in_line=0
                indexes=np.empty((0,))
                m=np.tan(theta*3.14159/180)
                if 0<=m<=1:
                    condition=True
                    while condition:

                        condition,index=extraction.check(np.array([y1,x1]),points)

                        if condition:
                            gap=0
                        else:
                            gap+=1
                            if gap<=max_gap:
                                condition=True
                        if condition:
                            in_line+=1
                            indexes=np.concatenate([indexes,np.array([index])])
                            x1+=1
                            y1+=m
                        else:
                            x1-=max_gap
                            y1-=m*max_gap




                    condition=True
                    while condition:
                        condition,index=extraction.check(np.array([y2,x2]),points)
                        if condition:
                            gap=0

                        else:
                            gap+=1

                            if gap<=max_gap:
                                condition=True
                        if condition:
                            indexes=np.concatenate([indexes,np.array([index])])
                            in_line+=1
                            x2-=1
                            y2-=m
                        else:
                            x2+=max_gap
                            y2+=m*max_gap



                elif 1<m<=np.tan(3.14159/2):
                    condition=True
                    while condition:
                        condition,index=extraction.check(np.array([y1,x1]),points)
                        if condition:
                            gap=0
                        else:
                            gap+=1
                            if gap<=max_gap:
                                condition=True
                        if condition:
                            in_line+=1
                            indexes=np.concatenate([indexes,np.array([index])])
                            x1+=1/m
                            y1+=1
                        else:
                            x1-=(1/m)*max_gap
                            y1-=max_gap



                    condition=True
                    while condition:
                        condition,index=extraction.check(np.array([y2,x2]),points)
                        if condition:
                            gap=0
                        else:
                            gap+=1
                            if gap<=max_gap:
                                condition=True
                        if condition:
                            indexes=np.concatenate([indexes,np.array([index])])
                            in_line+=1
                            x2-=1/m
                            y2-=1
                        else:
                            x2+=max_gap/m
                            y2+=max_gap


                elif np.tan(3.14159/2)<m<=np.tan(3.14159*3/4):
                    condition=True
                    while condition:
                        condition,index=extraction.check(np.array([y1,x1]),points)
                        if condition:
                            gap=0
                        else:
                            gap+=1
                            if gap<=max_gap:
                                condition=True
                        if condition:
                            indexes=np.concatenate([indexes,np.array([index])])
                            in_line+=1
                            x1-=1/m
                            y1+=1
                        else:
                            x1+=max_gap/m
                            y1-=max_gap


                    condition=True
                    while condition:
                        condition,index=extraction.check(np.array([y2,x2]),points)
                        if condition:
                            gap=0
                        else:
                            gap+=1

                            if gap<=max_gap:
                                condition=True
                        if condition:
                            indexes=np.concatenate([indexes,np.array([index])])

                            in_line+=1
                            x2+=1/m
                            y2-=1
                        else:
                            x2-=max_gap/m
                            y2+=max_gap

                else:
                    condition=True
                    while condition:
                        condition,index=extraction.check(np.array([y1,x1]),points)
                        if condition:
                            gap=0
                        else:
                            gap+=1
                            if gap<=max_gap:
                                condition=True
                        if condition:
                            indexes=np.concatenate([indexes,np.array([index])])
                            in_line+=1
                            x1-=1
                            y1+=m
                        else:
                            x1+=max_gap
                            y1-=m*max_gap


                    condition=True
                    while condition:
                        condition,index=extraction.check(np.array([y2,x2]),points)
                        if condition:
                            gap=0
                        else:
                            gap+=1
                        if gap<=max_gap:
                            condition=True
                        if condition:

                            indexes=np.concatenate([indexes,np.array([index])])
                            in_line+=1
                            x2+=1
                            y2-=m

                if mx_in_line<in_line:
                    mtheta=theta
                    mx_in_line=in_line
                    mindexes=indexes
                    valid[0]=np.array([int(x1),int(y1),int(x2),int(y2)])

            mindexes=np.sort(mindexes)[::-1]
            for j in mindexes[:-1]:
                try:
                    points=np.delete(points,int(j),axis=0)
                except:
                    pass
            x1,y1,x2,y2=valid[0].tolist()
            lines.append({'x1':x1,'x2':x2,'y1':y1,'y2':y2})
            # print(points.shape)
            if points.shape[0]==0:
                break
        extraction.data['floor']['walls']=lines
        # print('fjsk')
        with open(json_location+'line_cord.json','w') as f:
            # print('jfkds')
            json.dump(extraction.data,f)
        # print('fjjksk')

    @staticmethod
    def check(pt,points):
        y=pt[0]
        x=pt[1]
        if x-int(x)>0.5:
            x=int(x)+1
        else:
            x=int(x)
        if y-int(y)>0.5:
            y=int(y)+1
        else:
            y=int(y)
        pt[1]=x
        pt[0]=y
        index,=np.where(np.all(points==pt,axis=1))
        if len(index)==1:
            return True,index[0]
        else:
            return False,-1

if __name__=='__main__':
    imgJpeg = r"public/uploads/blueprint.jpeg"
    # imgJpg = r"public/uploads/blueprint.jpg"
    # imgPng = r"public/uploads/blueprint.png"
    # if imgJpeg:
    extraction.get_image(img_location=imgJpeg)
    # elif imgJpg:
    #     extraction.get_image(img_location=imgJpg)
    # elif imgPng:
    #     extraction.get_image(img_location=imgPng)
    # else:
    #     print('No image found')
    extraction.detecttext()
    extraction.detectline(json_location='',max_gap=3,threshold=50)

    data=[]
    with open('line_cord.json') as f:
        data=json.load(f)
    # print(data)
    # image=np.zeros((500,500))
    # for line in data['floor']['walls']:
    #     cv2.line(image, (int(line['x1']),int(line['y1'])), (int(line['x2']),int(line['y2'])), 255, 1)
    # for x in data['floor']['text_data']:
    #     # text_data.append({'text':text,'x':x+w//2,'y':y+h//2})
    #     image = cv2.putText(image,x['text'],(x['x'], x['y']),cv2.FONT_HERSHEY_SIMPLEX,0.5, (255, 255,255), 1)
    # cv2.imshow('a',image)
    # cv2.waitKey(0)
    # cv2.destroyAllWindows()

# In[ ]:





# In[ ]:





# In[ ]:
