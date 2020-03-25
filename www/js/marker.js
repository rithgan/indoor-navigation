var geojson = {
    'type': 'FeatureCollection',
    'features': [
        {
            'type': 'Feature',
            'properties': {
                'message': 'Dr. Akhilesh Das Gupta institute of technology and management. <a href="custom.html">View indoor map</a>',
                'iconSize': [15, 15]
            },
            'geometry': {
            'type': 'Point',
            'coordinates': [77.2612083,28.6791676]
            }
        },
        {
            'type': 'Feature',
            'properties': {
                'message': 'Block 2. IT department',
                'iconSize': [15, 15]
            },
            'geometry': {
            'type': 'Point',
            'coordinates': [77.2610256,28.6784562]
            }
        },
        {
            'type': 'Feature',
            'properties': {
                'message': 'Block 4. CSE department',
                'iconSize': [15, 15]
            },
            'geometry': {
            'type': 'Point',
            'coordinates': [77.2609000,28.6780966]
            }
        },
        {
            'type': 'Feature',
            'properties': {
                'message': 'Block 3. ECE department',
                'iconSize': [15, 15]
            },
            'geometry': {
            'type': 'Point',
            'coordinates': [77.2612484,28.6780184]
            }
        },
        {
            'type': 'Feature',
            'properties': {
                'message': 'Block 5. ADMIN block, BBA department',
                'iconSize': [15, 15]
            },
            'geometry': {
            'type': 'Point',
            'coordinates': [77.2607405,28.6778073]
            }
        }
    ]
};

 
// add markers to map
geojson.features.forEach(function(marker) {
    // create a DOM element for the marker
    var el = document.createElement('div');
    el.className = 'marker';
    el.style.backgroundImage =
    'url(https://placekitten.com/g/' +
    marker.properties.iconSize.join('/') +
    '/)';
    el.style.width = marker.properties.iconSize[0] + 'px';
    el.style.height = marker.properties.iconSize[1] + 'px';
     
    // el.addEventListener('click', function() {
    // window.alert(marker.properties.message);
    // });

    // create DOM element for the marker
    var ele = document.createElement('div');
    ele.id = 'marker';

     
    var popup = new mapboxgl.Popup({ offset: 25 }).setHTML(
        marker.properties.message
    );
         

    // add marker to map
    new mapboxgl.Marker(el)
    .setLngLat(marker.geometry.coordinates)
    .setPopup(popup)
    .addTo(map);
    });



// -------------------------------------------------------------------

// mapboxgl.accessToken = 'pk.eyJ1IjoiZ29vZGZlZWwiLCJhIjoiY2s1dzNpbWhnMGhlaTNrcnZocHZrNWxzaCJ9.2v1JUkoGrDg4GJJ8ko2yUQ';
// // var college = [-77.0353, 38.8895];
// // var map = new mapboxgl.Map({
// // container: 'map',
// // style: 'mapbox://styles/mapbox/light-v10',
// // center: college,
// // zoom: 15
// // });
 
// // create the popup
// var popup = new mapboxgl.Popup({ offset: 25 }).setHTML(
// 'Dr. Akhilesh das gupta institute of technology was founed on 2003. <a href="https://en.wikipedia.org/wiki/Dr._Akhilesh_Das_Gupta_Institute_of_Technology_and_Management">Learn More</a>'
// );
 
// // create DOM element for the marker
// var ele = document.createElement('div');
// ele.id = 'marker';
 
// // create the marker
// new mapboxgl.Marker(ele)
// .setLngLat(77.2612083,28.6791676)
// // .setLngLat(college)
// .setPopup(popup) // sets a popup on this marker
// .addTo(map);