(() => {
    window.onload = function () {
        var type = document.getElementById('typeId');
        var shape = document.getElementById('shapeId');
        var manufacturer = document.getElementById('manufacturerId');
        var finenessId = document.getElementById('finenessId');

        changeFineness(finenessId.value);
        document.getElementById('Type').options[type.value].selected = true;
        document.getElementById('Shape').options[shape.value].selected = true;
        document.getElementById('Manufacturer').options[manufacturer.value - 1].selected = true;
    }

    function changeFineness(value) {
        var fineness = document.getElementById('Fineness');

        for (var i = 0; i < fineness.options.length; i++) {
            if (fineness.options[i].value == value) {
                fineness.options[i].selected = true;
            }
        }
    }
})()