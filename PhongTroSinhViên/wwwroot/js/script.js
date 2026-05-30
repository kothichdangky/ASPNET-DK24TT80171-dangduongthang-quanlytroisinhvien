const slides = document.querySelectorAll(".slide");
const dots = document.querySelectorAll(".dot");

const loginBtn = document.querySelector(".login-btn");

const hired = document.querySelector(".hire");

const requests = document.querySelector(".request");

const checks = document.querySelector(".check");

const settings = document.querySelector(".setting");

const details = document.querySelector(".detail");

//  modals 

const loginModal = document.querySelector(".login-modal");

const paymentModal = document.querySelector(".payment-modal");

const fixModal = document.querySelector(".fix-modal");

const checkModal = document.querySelector(".check-modal");

const invoiceModal = document.querySelector(".invoice-modal");

const settingModal = document.querySelector(".setting-modal");

const detailModal = document.querySelector(".detail-modal");


//  buttons 

const confirmPayment = document.querySelector(".confirm-payment");

const invoiceBtn = document.querySelector(".invoice-btn");

// room select

const phongSelect = document.getElementById("phongSelect");

const tienPhong = document.getElementById("tienPhong");

const tienCoc = document.getElementById("tienCoc");

const tongTien = document.getElementById("tongTien");


//  carousel 

let current = 0;

function showSlide(index){

  if(!slides[index]) return;

  slides.forEach(slide=>{
    slide.classList.remove("active");
  });

  dots.forEach(dot=>{
    dot.classList.remove("active");
  });

  slides[index].classList.add("active");

  if(dots[index]){
    dots[index].classList.add("active");
  }

}

//  click dots 

dots.forEach((dot,index)=>{

  dot.addEventListener("click", ()=>{

    current = index;

    showSlide(current);

  });

});

//  auto slide 

if(slides.length > 0){

  setInterval(()=>{

    current++;

    if(current >= slides.length){
      current = 0;
    }

    showSlide(current);

  },5000);

}

//  open login 

if(loginBtn){

  loginBtn.addEventListener("click", ()=>{

    loginModal.classList.add("active");

  });

}

//  open payment 

if(hired){

  hired.addEventListener("click", ()=>{

    paymentModal.classList.add("active");

  });

}

//  open fix 

if(requests){

  requests.addEventListener("click", ()=>{

    fixModal.classList.add("active");

  });

}

//  open check 

if(checks){

  checks.addEventListener("click", ()=>{
    checkModal.classList.add("active");
  });

}

//  open setting

if(settings){

  settings.addEventListener("click", ()=>{
    settingModal.classList.add("active");
  });

}

//  open detail

if(details){

  details.addEventListener("click", ()=>{
    detailModal.classList.add("active");
  });

}

//  close modals

document.querySelectorAll(".close-modal").forEach(btn=>{

  btn.addEventListener("click", ()=>{

    btn.closest(".modal").classList.remove("active");

  });

});

//  click outside

document.querySelectorAll(".modal").forEach(modal=>{

  modal.addEventListener("click", (e)=>{

    if(e.target === modal){

      modal.classList.remove("active");

    }

  });

});

//  confirm payment

if(confirmPayment){

  confirmPayment.addEventListener("click", ()=>{

    paymentModal.classList.remove("active");

    invoiceModal.classList.add("active");

  });

}

//  close invoice

if(invoiceBtn){

  invoiceBtn.addEventListener("click", ()=>{

    invoiceModal.classList.remove("active");

  });

}


// Room select
if (
  phongSelect &&
  tienPhong &&
  tienCoc &&
  tongTien
) {

  function capNhatThongTinPhong() {

    const option =
      phongSelect.options[phongSelect.selectedIndex];

    const tienHangThang =
      Number(option.dataset.tienhangthang);

    const tienDatCoc =
      Number(option.dataset.tiendatcoc);

    tienPhong.textContent =
      tienHangThang.toLocaleString() + " đ";

    tienCoc.textContent =
      tienDatCoc.toLocaleString() + " đ";

    tongTien.textContent =
      (tienHangThang + tienDatCoc)
      .toLocaleString() + " đ";

  }

  phongSelect.addEventListener(
    "change",
    capNhatThongTinPhong
  );

  capNhatThongTinPhong();
}