const fileProfile = document.getElementById("fileProfile");
const preview = document.getElementById("PreviewPicture")

fileProfile.addEventListener('change', (evt => {
    var file = evt.target.files[0]
    preview.src = URL.createObjectURL(file);
}))
