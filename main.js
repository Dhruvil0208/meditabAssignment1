function resetFunction() {
    document.getElementById("mainform").reset();
}



    const form = document.getElementById("mainform");

    form.addEventListener('submit', (e) => {
        e.preventDefault();
        const fd = new FormData(form);
        const entries = fd.entries();
        const data = Object.fromEntries(entries);
        console.log(data)
        
    })



function checkAge(){
    var inputDate = document.getElementById("dob").value;
    var dob = new Date(inputDate)
    var dobYear = dob.getFullYear()
    
    var today = new Date();
    var year = today.getFullYear()
    
    var age = year - dobYear;
    
    if(age < 18){
        alert("The Patient is minor please enter parent's contact number.")
    }
}