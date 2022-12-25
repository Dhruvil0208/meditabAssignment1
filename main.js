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


const sidebar = document.querySelector('.sidebar');
const mainform = document.querySelector('.mainform');

document.querySelector('.toggle').onclick = function()
{
    sidebar.classList.toggle('sidebar-small');
    mainform.classList.toggle('mainform-large');
}



var coll = document.getElementsByClassName("other-details-header");
var i;

for (i = 0; i < coll.length; i++) {
console.log(i);
  coll[i].addEventListener("click", function() {
    
    var content = this.nextElementSibling;
    if (content.style.display === "block") {
      content.style.display = "none";
    } else {
      content.style.display = "block";
    }
  });
}