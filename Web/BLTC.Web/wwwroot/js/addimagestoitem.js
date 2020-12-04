(() => {
    var totalFiles = [];

    function handleFileSelect(evt) {
        var files = evt.target.files; // FileList object
        
        // Loop through the FileList and render image files as thumbnails.
        for (var i = 0, f; f = files[i]; i++) {

            // Only process image files.
            if (!f.type.match('image.*')) {
                continue;
            }

            totalFiles.push(f)

            var reader = new FileReader();

            // Closure to capture the file information.
            reader.onload = (function (theFile) {
                return function (e) {
                    // Render thumbnail.

                    var span = document.createElement('span');
                    span.innerHTML = ['<img class="img-fluid rounded shadow-sm mx-auto d-block" id="image" src="', e.target.result,
                        '" title="', escape(theFile.name), '"/>', "<button class=\"btn btn-danger form-group float-right\" onclick=\"deleteImage()\">DELETE</button></br>"
                    ].join('');

                    document.getElementById('list').insertBefore(span, null);
                };
            })(f);

            // Read in the image file as a data URL.
            reader.readAsDataURL(f);
        }
    }

    document.getElementById('Files').addEventListener('change', handleFileSelect, false);
})();