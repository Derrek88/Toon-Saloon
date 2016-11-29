$(function () {
    var images = ['CN.jpg', 'rickandMorty.jpg' , 'simpsons.jpg', 'transformers.jpg', 'disney.jpg', 'comics.jpg', 'random.jpg'];
    $('#background').css({ 'background-image': 'url(../../Backgrounds/' + images[Math.floor(Math.random() * images.length)] + ')' });
});
