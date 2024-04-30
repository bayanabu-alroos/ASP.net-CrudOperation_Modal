function handleEditButtonClick(button) {
    const row = button.closest('tr');
    const itemIdCell = row.querySelector('#itemId');
    const itemId = itemIdCell.innerHTML.trim();
    document.getElementById("selectedMangeItemId").value = itemId;
    //Name
    const itemNameCell = row.querySelector('#itemName');
    const itemName = itemNameCell.innerHTML.trim();
    document.getElementById("selectedItemName").value = itemName;
    //Description
    const itemDescriptionCell = row.querySelector('#itemDesc');
    const itemDesc = itemDescriptionCell.innerHTML.trim();
    document.getElementById("selectedItemDescription").value = itemDesc;
    //Status
    const itemImageUrlCell = row.querySelector('#itemImageUrl>img');
    const src = itemImageUrlCell.getAttribute('src');
    console.log(src)
    const itemImageUrl = src.innerHTML.trim();
    document.getElementById("selectedItemImageUrl").value = itemImageUrl;
}

function handleDeleteButtonClick(button) {
    const row = button.closest('tr');
    const itemIdCell = row.querySelector('#itemId');
    const itemId = itemIdCell.innerHTML.trim();
    document.getElementById("selectedItemId").value = itemId;
}