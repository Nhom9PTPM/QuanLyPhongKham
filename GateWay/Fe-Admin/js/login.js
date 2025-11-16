async function Login(username, password) {
  const messageDiv = document.getElementById("message");
  messageDiv.innerHTML = "";

  try {
    const response = await fetch("https://localhost:44340/api/users/login", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ username, password })
    });

    if (!response.ok) {
      const err = await response.json().catch(() => ({}));
      messageDiv.className = "error";
      messageDiv.innerText = err.message || "Đăng nhập thất bại";
      return;
    }

    const data = await response.json();
    const token = data.token;
    localStorage.setItem("token", token);

    messageDiv.className = "success";
    messageDiv.innerText = "Đăng nhập thành công!";

    // Chuyển trang sau 500ms
    setTimeout(() => {
      window.location.href = "index.html";
    }, 500);

  } catch (error) {
    console.error(error);
    messageDiv.className = "error";
    messageDiv.innerText = "Không thể kết nối tới server.";
  }
}

// Bắt sự kiện submit form
document.getElementById("loginForm").addEventListener("submit", function(e) {
  e.preventDefault(); // chặn reload trang
  const username = document.getElementById("taikhoan").value;
  const password = document.getElementById("matkhau").value;
  Login(username, password);
});
