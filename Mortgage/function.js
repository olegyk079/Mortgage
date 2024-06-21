function calculate() {
    var amount = document.getElementById("amount");
    var apr = document.getElementById("apr");
    var years = document.getElementById("years");
    var res = validate(amount, apr, years);
    if (!res) {
        return;
    }
    var payment = document.getElementById("payment");
    var total = document.getElementById("total");
    var totalinterest = document.getElementById("totalinterest");
    var principal = parseFloat(amount.value);
    var interest = parseFloat(apr.value) / 100 / 12;
    var payments = parseFloat(years.value) * 12;
    var x = Math.pow(1 + interest, payments);
    var monthly = (principal * x * interest) / (x - 1);

    var paymentRes = monthly.toFixed(2);
    var totalRes = (monthly * payments).toFixed(2);
    var totalInterestRes = ((monthly * payments) - principal).toFixed(2);

    ShowResult(monthly, paymentRes, totalRes, totalInterestRes);
}

function ShowResult(monthly, paymentRes, totalRes, totalInterestRes) {
    if (isFinite(monthly)) {
        payment.innerHTML = paymentRes;
        total.innerHTML = totalRes;
        totalinterest.innerHTML = totalInterestRes;

        save(amount.value, apr.value, years.value);
    } else {
        payment.innerHTML = "";
        total.innerHTML = "";
        totalinterest.innerHTML = "";
    }
}

function validate(amount, apr, years) {
    if (!amount.value) {
        alert("Poproszę wprowadzić wartość kredytu");
        return false;
    }if (!/^\d*\.?\d*$/.test(amount.value)) {
        alert("Poproszę wprowadzić tylko cyfry dla wartości kredytu");
        return false;
    }if (!apr.value) {
        alert("Poproszę wprowadzić roczne odsetki");
        return false;
    }if (!/^\d*\.?\d*$/.test(apr.value)) {
        alert("Poproszę wprowadzić tylko cyfry dla rocznych odsetek");
        return false;
    }if (!years.value) {
        alert("Poproszę wprowadzić okres spłaty");
        return false;
    }if (!/^\d*\.?\d*$/.test(years.value)) {
        alert("Poproszę wprowadzić tylko cyfry dla okresu spłaty");
        return false;
    }
    return true;
}

function validateInput(input) {
    if (input.value && !/^\d*\.?\d*$/.test(input.value)) {
        alert("Proszę wprowadzić tylko cyfry");
        input.value = input.value.slice(0, -1);
    }
}

function save(amount, apr, years) {
    if (window.localStorage) {
        localStorage.loan_amount = amount;
        localStorage.loan_apr = apr;
        localStorage.loan_years = years;
    }
}

window.onload = function() {
    if (window.localStorage && localStorage.loan_amount) {
        document.getElementById("amount").value = localStorage.loan_amount;
        document.getElementById("apr").value = localStorage.loan_apr;
        document.getElementById("years").value = localStorage.loan_years;
    }
}