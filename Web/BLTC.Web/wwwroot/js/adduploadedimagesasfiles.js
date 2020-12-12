(() => {
    document.getElementById('btns-container').addEventListener('mouseenter', function () {
        var currentFiles = document.getElementById('Files');
        var images = document.querySelectorAll('#image');

        if (currentFiles.files.length != images.length) {
            var arr = new DataTransfer();

            async function getImageBlob(imageUrl, fileName) {
                const response = await fetch(imageUrl)
                return (fetch(imageUrl)
                    .then(function (res) { return res.arrayBuffer(); })
                    .then(function (buf) { return new File([buf], fileName, { type: response.blob().type }); })
                );
            }

            for (let i = 0; i < images.length; i++) {
                var src = images[i].src;
                var fileName = images[i].title;

                var blob = getImageBlob(src, fileName);
                blob.then(function (x) {
                    arr.items.add(x); // adds both image files
                    currentFiles.files = arr.files;
                });

            }
        }
    });
})();