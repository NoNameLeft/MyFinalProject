(() => {
    $(document).ready(function () {
        // Create Stripe client.
        var stripe = Stripe('pk_test_51I19d5DzCsvWcQCqPoZWUutFWjJO9XPZhHu2aXOFoQvRC8fe5K5IchJ7tsHW7xHfBSM7635AQLc6Gt2Yjj7hToQW00Yad0CVBG');

        // Create an instance of Elements.
        var elements = stripe.elements();

        var style = {
            base: {
                'fontSize': '18px',
                'color': '#49057'
            },
        };

        // Card number
        var card = elements.create('cardNumber', {
            'placeholder': '',
            'style': style
        });
        card.mount('#card-number');

        // CVC
        var cvc = elements.create('cardCvc', {
            'placeholder': '',
            'style': style
        });
        cvc.mount('#card-cvc');

        // Card expiry
        var exp = elements.create('cardExpiry', {
            'placeholder': '',
            'style': style
        });
        exp.mount('#card-exp');

        // Handle subbmision.
        var form = document.getElementById('payment-form');
        form.addEventListener('submit', function () {
            event.preventDefault();
            stripe.createToken(card).then(function (result) {
                if (result.error) {
                    // Inform user in case of error.
                    var errorElement = document.getElementById('card-errors');
                    errorElement.textContent = result.error.message;
                }
                else {
                    // Send the token to your server.
                    stripeTokenHandler(result.token);
                }
            });
        });

        // Submit the form with the token ID.
        function stripeTokenHandler(token) {
            // Insert the token ID into form so it gets submitted.
            var stripeToken = document.querySelector('.StripeToken');
            stripeToken.value = token.id;
            // Submit the form.
            form.submit();
        }
    });
})()