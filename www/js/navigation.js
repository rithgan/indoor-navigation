// Navigation control gives compass and zoom in and zoom out options
var nav = new mapboxgl.NavigationControl();
map.addControl(nav, 'bottom-right');

var geocoder = new MapboxGeocoder({
    accessToken: mapboxgl.accessToken,
    mapboxgl: mapboxgl
    });
     
    document.getElementById('geocoder').appendChild(geocoder.onAdd(map));