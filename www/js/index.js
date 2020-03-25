mapboxgl.accessToken = 'pk.eyJ1IjoiZ29vZGZlZWwiLCJhIjoiY2s1dzNpbWhnMGhlaTNrcnZocHZrNWxzaCJ9.2v1JUkoGrDg4GJJ8ko2yUQ';
var map = new mapboxgl.Map({
container: 'map', // container id
style: 'mapbox://styles/mapbox/streets-v10', // stylesheet location
center: [77.1025, 28.7041], // starting position [lng, lat]
zoom: 10 // starting zoom
});

// // This add the search box
// map.addControl(
//     new MapboxGeocoder({
//     accessToken: mapboxgl.accessToken,
//     mapboxgl: mapboxgl
//     })
//     );




