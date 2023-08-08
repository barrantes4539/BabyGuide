document.addEventListener("DOMContentLoaded", function () {
    const consejosLink = document.getElementById("consejosLink");
    const nombresLink = document.getElementById("nombresLink");
    const expedienteLink = document.getElementById("expedienteLink");

    const consejosForm = document.getElementById("consejosForm");
    const nombresForm = document.getElementById("nombresForm");
    const expedienteForm = document.getElementById("expedienteForm");
    const configForm = document.getElementById("expedienteForm");

    consejosLink.addEventListener("click", function () {
        consejosForm.style.display = "block";
        nombresForm.style.display = "none";
        expedienteForm.style.display = "none";
    });

    nombresLink.addEventListener("click", function () {
        consejosForm.style.display = "none";
        nombresForm.style.display = "block";
        expedienteForm.style.display = "none";
    });

    expedienteLink.addEventListener("click", function () {
        consejosForm.style.display = "none";
        nombresForm.style.display = "none";
        expedienteForm.style.display = "block";
    });
});