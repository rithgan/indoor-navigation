#!/usr/bin/env python
# coding: utf-8

# In[1]:


import cv2
import numpy as np
import json
from pytesseract import *


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
