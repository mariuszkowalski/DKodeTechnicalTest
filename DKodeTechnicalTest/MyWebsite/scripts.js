$(document).ready(function () {
    // __INIT__
    let currentInput = '0';
    let currentOperation = null;
    let previousInput = null;
    let currentCurrency = 'PLN';
    let rates = {
        EUR: null,
        USD: null,
        GBP: null
    };

    function fetchRates() {
        const targetDate = '2025-02-03';
        // -- RATES -- 
        $.ajax({
            url: `https://api.nbp.pl/api/exchangerates/rates/a/eur/${targetDate}/?format=json`,
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                rates.EUR = data.rates[0].mid;
                $('#eur-rate').text(rates.EUR.toFixed(4));
            },
            error: function () {
                $('#eur-rate').text('Error');
            }
        });

        $.ajax({
            url: `https://api.nbp.pl/api/exchangerates/rates/a/usd/${targetDate}/?format=json`,
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                rates.USD = data.rates[0].mid;
                $('#usd-rate').text(rates.USD.toFixed(4));
            },
            error: function () {
                $('#usd-rate').text('Error');
            }
        });

        $.ajax({
            url: `https://api.nbp.pl/api/exchangerates/rates/a/gbp/${targetDate}/?format=json`,
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                rates.GBP = data.rates[0].mid;
                $('#gbp-rate').text(rates.GBP.toFixed(4));
            },
            error: function () {
                $('#gbp-rate').text('Error');
            }
        });
    }

    fetchRates();

    function updateDisplay() {
        let displayValue = currentInput;

        if (!isNaN(parseFloat(displayValue)) && displayValue.length > 10) {
            displayValue = parseFloat(displayValue)
        }

        $('#result').text(displayValue);
    }

    function setCurrencyIndicator(currency) {
        $('.currency-indicator').text(currency);
    }

    // Number button click
    $('.number').click(function () {
        const value = $(this).text();

        // Handle decimal point
        if (value === '.' && currentInput.includes('.')) {
            return;
        }

        // Handle first input or reset state
        if (currentInput === '0' && value !== '.') {
            currentInput = value;
        } else {
            currentInput += value;
        }

        updateDisplay();
    });

    // on Operation click.
    $('.operation').click(function () {
        const operation = $(this).text();

        // do calculation in case of input
        if (previousInput !== null && currentOperation !== null) {
            calculate();
        }

        previousInput = parseFloat(currentInput);
        currentOperation = operation;
        currentInput = '0';
    });

    // CLEAR and RESET...
    $('.clear').click(function () {
        currentInput = '0';
        currentOperation = null;
        previousInput = null;
        currentCurrency = 'PLN';
        setCurrencyIndicator('PLN');
        updateDisplay();
    });

    // Equals button click
    $('.equals').click(function () {
        calculate();
    });

    function calculate() {
        const current = parseFloat(currentInput);
        const previous = parseFloat(previousInput);
        let result;

        if (isNaN(previous) || isNaN(current)) return;

        switch (currentOperation) {
            case '+':
                result = previous + current;
                break;
            case '-':
                result = previous - current;
                break;
            case '*': // This should be "X"!!!!!
                result = previous * current;
                break;
            case '/':
                if (current === 0) {
                    result = 'Error';
                } else {
                    result = previous / current;
                }
                break;
            default:
                return;
        }

        currentInput = result.toString();
        currentOperation = null;
        previousInput = null;
        updateDisplay();
    }

    // -- CURRENCY CONVERSION --
    $('.currency').click(function () {
        const targetCurrency = $(this).text();

        if (rates.EUR === null || rates.USD === null || rates.GBP === null) {
            return;
        }

        if (targetCurrency === currentCurrency) {
            return;
        }

        let valueInPLN;

        // First convert current value to PLN if needed
        if (currentCurrency === 'PLN') {
            valueInPLN = parseFloat(currentInput);
        } else if (currentCurrency === 'EUR') {
            valueInPLN = parseFloat(currentInput) * rates.EUR;
        } else if (currentCurrency === 'USD') {
            valueInPLN = parseFloat(currentInput) * rates.USD;
        } else if (currentCurrency === 'GBP') {
            valueInPLN = parseFloat(currentInput) * rates.GBP;
        }

        // Then convert from PLN to target currency
        if (targetCurrency === 'PLN') {
            currentInput = valueInPLN.toString();
        } else if (targetCurrency === 'EUR') {
            currentInput = (valueInPLN / rates.EUR).toString();
        } else if (targetCurrency === 'USD') {
            currentInput = (valueInPLN / rates.USD).toString();
        } else if (targetCurrency === 'GBP') {
            currentInput = (valueInPLN / rates.GBP).toString();
        }

        currentCurrency = targetCurrency;
        setCurrencyIndicator(targetCurrency);
        updateDisplay();
    });
});