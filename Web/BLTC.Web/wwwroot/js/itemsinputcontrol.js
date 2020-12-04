(() => {
    window.onload = function () {
        var type = document.getElementById('Type');
        var shape = document.getElementById('Shape');

        type.addEventListener('click', function () {
            var purityInput = document.getElementById('Purity');
            var finenessInput = document.getElementById('Fineness');
            var purity = document.getElementById('puritylabel');

            type.options[type.selectedIndex].selected = true;
            var text = type.options[type.selectedIndex].text;
            if (text == "Currency") {
                purity.innerHTML = "Precious metal content (grams):";
                purity.setAttribute("value", "0");
                finenessInput.value = 0;
                purityInput.setAttribute("disabled", "true");
                finenessInput.setAttribute("disabled", "true");
                shape.options["2"].selected = true;
                shape.options["0"].disabled = true;
                shape.options["2"].disabled = false;
            }
            else {
                purity.innerHTML = "Pure " + text + " content (grams):";
                if (purityInput != null) {
                    purityInput.removeAttribute("disabled");
                    finenessInput.removeAttribute("disabled");
                    shape.options["0"].selected = true;
                    shape.options["0"].disabled = false;
                    shape.options["2"].disabled = true;
                }
            }
        });
    }
})();