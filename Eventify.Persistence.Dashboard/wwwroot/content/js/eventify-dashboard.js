
function openModal(modalName) {
    $('#' + modalName).modal('show')
}

function closeModal(modalName) {
    $('#' + modalName).modal('hide')
}

async function copyToClipboard(text) {
    await navigator.clipboard.writeText(text);
}
