(() => {
    window.addEventListener('load', function () {
        const maybePluralize = (count, noun, suffix = 's') =>
            `${count} ${noun}${count !== 1 ? suffix : ''}`;

        var title = document.getElementById('title');
        var words = title.innerHTML.split(' ');
        if (words[1] !== 'Items') {
            words[1] = words[1] + 's';
            title.innerHTML = words[0].concat(' ', words[1]);
        }
    })
})();