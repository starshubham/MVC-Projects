function confirmDelete(uniqueId, isDeleteClicked) {
    var deletespan = 'deletespan_' + uniqueId;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + uniqueId;

    if (isDeleteClicked) {
        $('#' + deletespan).hide();
        $('#' + confirmDeleteSpan).show();
    }
    else {
        $('#' + confirmDeleteSpan).hide();
        $('#' + deletespan).show();
    }

}