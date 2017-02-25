var ThisAndThat = (function () {
    var privateVariable = "Hallo Welt";
    function privateFunction() {
        alert(privateVariable);
    }

    return {
        showMessageHalloWelt: function () {
            privateFunction();
        },
        variable: "exoplanet"
    };
})();
