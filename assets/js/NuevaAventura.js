
let files = [],
    form = document.querySelector('.drag-drop-container'),
    container = document.querySelector('.contenedor'),
    Text = document.querySelector('.inner'),
    browse = document.querySelector('.select'),
    input = document.querySelector('.file');

browse.addEventListener('click', () => input.click());

//input change event
input.addEventListener('change', () => {
    let file = input.files;

    for (let i = 0; i < file.length; i++) {
        if (files.every(e => e.name != file[i].name)) files.push(file[i])
    }

    showImages();
})

const showImages = () => {
    let images = '';
    files.forEach((e, i) => {
        images += `<div class="img">
                <img src="${URL.createObjectURL(e)}" alt="image">
                <span onclick="delImage(${i})">&times;</span>
            </div>`
    })
    container.innerHTML = images;
}

const delImage = index => {
    files.splice(index, 1)
    showImages()
}

//Drag and Drop
form.addEventListener('dragover', e => {
    e.preventDefault()

    form.classList.add('dragover')
    Text.innerHTML = 'Drop image here'
})

form.addEventListener('dragleave', e => {
    e.preventDefault()

    form.classList.remove('dragover')
})

form.addEventListener('drop', e => {
    e.preventDefault();

    if (e.dataTransfer.items) {
        // Use DataTransferItemList interface to access the dropped items
        for (let i = 0; i < e.dataTransfer.items.length; i++) {
            if (e.dataTransfer.items[i].kind === 'file') {
                let file = e.dataTransfer.items[i].getAsFile();
                if (files.every(e => e.name !== file.name)) {
                    files.push(file);
                    break; // Stop after adding the first image
                }
            }
        }
    } else {
        // Use legacy DataTransfer interface to access the dropped files
        for (let i = 0; i < e.dataTransfer.files.length; i++) {
            let file = e.dataTransfer.files[i];
            if (files.every(e => e.name !== file.name)) {
                files.push(file);
                break; // Stop after adding the first image
            }
        }
    }

    showImages();
});