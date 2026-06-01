const slides = document.querySelectorAll(".slide");
const dots = document.querySelectorAll(".dot");

const loginBtn = document.querySelector(".login-btn");

const hired = document.querySelector(".hire");

const hired_2 = document.querySelector(".hire-2");

const requests = document.querySelector(".request");

const checks = document.querySelector(".check");

const settings = document.querySelector(".setting");

const details = document.querySelector(".detail");

//  modals

const loginModal = document.querySelector(".login-modal");

const paymentModal = document.querySelector(".payment-modal");

const paymentModal_2 = document.querySelector(".payment-modal-2");

const fixModal = document.querySelector(".fix-modal");

const checkModal = document.querySelector(".check-modal");

const invoiceModal = document.querySelector(".invoice-modal");

const settingModal = document.querySelector(".setting-modal");

const detailModal = document.querySelector(".detail-modal");

//  buttons

const confirmPayment = document.querySelector(".confirm-payment");

const confirmPayment_2 = document.querySelector(".confirm-payment-2");

const invoiceBtn = document.querySelector(".invoice-btn");

// room select

const phongSelect = document.getElementById("phongSelect");

const tienPhong = document.getElementById("tienPhong");

const tienCoc = document.getElementById("tienCoc");

const tongTien = document.getElementById("tongTien");

//  carousel

let current = 0;

function showSlide(index) {
  if (!slides[index]) return;

  slides.forEach((slide) => {
    slide.classList.remove("active");
  });

  dots.forEach((dot) => {
    dot.classList.remove("active");
  });

  slides[index].classList.add("active");

  if (dots[index]) {
    dots[index].classList.add("active");
  }
}

//  click dots

dots.forEach((dot, index) => {
  dot.addEventListener("click", () => {
    current = index;

    showSlide(current);
  });
});

//  auto slide

if (slides.length > 0) {
  setInterval(() => {
    current++;

    if (current >= slides.length) {
      current = 0;
    }

    showSlide(current);
  }, 2000);
}

//  open login

if (loginBtn) {
  loginBtn.addEventListener("click", () => {
    loginModal.classList.add("active");
  });
}

//  open payment

if (hired) {
  hired.addEventListener("click", () => {
    paymentModal.classList.add("active");
  });
}

//  open payment 2

if (hired_2) {
  hired_2.addEventListener("click", () => {

    document.getElementById("tienPhong")
      .textContent =
      hired.dataset.tienphong + " đ";

    document.getElementById("luongnuoc")
      .textContent =
      hired.dataset.nuoc + " đ";

    document.getElementById("luongdien")
      .textContent =
      hired.dataset.dien + " đ";
    paymentModal_2.classList.add("active");
  });
}

//  open fix

if (requests) {
  requests.addEventListener("click", () => {
    fixModal.classList.add("active");
  });
}

//  open check

if (checks) {
  checks.addEventListener("click", () => {
    checkModal.classList.add("active");
  });
}

//  open setting

if (settings) {
  settings.addEventListener("click", () => {
    settingModal.classList.add("active");
  });
}

//  open detail

if (details) {
  details.addEventListener("click", () => {
    detailModal.classList.add("active");
  });
}

//  close modals

document.querySelectorAll(".close").forEach((btn) => {
  btn.addEventListener("click", () => {
    btn.closest(".modal").classList.remove("active");
  });
});

//  click outside

document.querySelectorAll(".modal").forEach((modal) => {
  modal.addEventListener("click", (e) => {
    if (e.target === modal) {
      modal.classList.remove("active");
    }
  });
});

//  confirm payment

if (confirmPayment) {
  confirmPayment.addEventListener("click", () => {
    paymentModal.classList.remove("active");

    invoiceModal.classList.add("active");
  });
}

if (confirmPayment_2) {
  confirmPayment_2.addEventListener("click", () => {
    paymentModal_2.classList.remove("active");

    invoiceModal.classList.add("active");
  });
}

//  close invoice

if (invoiceBtn) {
  invoiceBtn.addEventListener("click", () => {
    invoiceModal.classList.remove("active");
  });
}

// Room select
if (phongSelect && tienPhong && tienCoc && tongTien) {
  function capNhatThongTinPhong() {
    const option = phongSelect.options[phongSelect.selectedIndex];

    const tienHangThang = Number(option.dataset.tienhangthang);

    const tienDatCoc = Number(option.dataset.tiendatcoc);

    tienPhong.textContent = tienHangThang.toLocaleString() + " đ";

    tienCoc.textContent = tienDatCoc.toLocaleString() + " đ";

    tongTien.textContent = (tienHangThang + tienDatCoc).toLocaleString() + " đ";
  }

  phongSelect.addEventListener("change", capNhatThongTinPhong);

  capNhatThongTinPhong();
}

// setting modal
document.querySelectorAll(".setting").forEach((btn) => {
  btn.addEventListener("click", () => {
    document.getElementById("PhongId").value = btn.dataset.id;

    document.getElementById("DeletePhongId").value = btn.dataset.id;

    document.getElementById("TenNguoiThue").value = btn.dataset.ten;

    document.getElementById("LuongNuoc").value = btn.dataset.nuoc;

    document.getElementById("LuongDien").value = btn.dataset.dien;

    document.getElementById("TienHangThang").value = btn.dataset.tien;

    document.getElementById("TinhTrangDongTien").value =
      btn.dataset.dongtien.toLowerCase();
    settingModal.classList.add("active");
  });
});
