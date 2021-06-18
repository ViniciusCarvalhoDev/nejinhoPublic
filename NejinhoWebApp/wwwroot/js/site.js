// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function formatDateFromCSgarp(dataObject) {
    let novaData = new Date(dataObject)
    return novaData.getDate().toString().padStart(2, '0') +
        "/" + (novaData.getMonth() + 1).toString().padStart(2, '0') +
        "/" + novaData.getFullYear().toString().padStart(4, '0');
}

function formatDateAndTimeFromCSgarp(dataObject) {
    let novaData = new Date(dataObject)
    return novaData.getDate().toString().padStart(2, '0') +
        "/" + (novaData.getMonth() + 1).toString().padStart(2, '0') +
        "/" + novaData.getFullYear().toString().padStart(4, '0') + " às " +
        novaData.getHours().toString() + " horas e " + novaData.getMinutes().toString() + " minutos";
}