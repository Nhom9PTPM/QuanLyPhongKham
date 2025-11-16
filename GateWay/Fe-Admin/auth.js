// auth.js
(function () {
  'use strict';

  document.addEventListener('DOMContentLoaded', function () {
    var isLoggedIn = localStorage.getItem('isLoggedIn');
    var token = localStorage.getItem('token');
    var role = parseInt(localStorage.getItem('role'), 10);

    if (!isLoggedIn || !token || !Number.isInteger(role)) {
      if (!/index\.html?$/i.test(window.location.pathname)) {
        window.location.href = '../pages/index.html';
      }
      return;
    }

    // Hiển thị tên người dùng
    try {
      var user = JSON.parse(localStorage.getItem('user') || '{}');
      var name = user.userName || user.username || 'PK';
      var strong = document.querySelector('.quick-stats .row strong');
      if (strong) strong.textContent = name;
    } catch (_) {}

    // Quyền -> whitelist
    var allowByRole = {
      4: ['QuanLyPhongKham','QuanLyBacSi','QuanLyThuNgan','QuanLyBenhNhan'],
      3: ['QuanLyBacSi'],
      2: ['QuanLyThuNgan'],
      1: ['QuanLyBenhNhan']
    };
    var defaultPageByRole = {
      4: '../pages/QuanLyPhongKham.html',
      3: '../pages/QuanLyBacSi.html',
      2: '../pages/QuanLyThuNgan.html',
      1: '../pages/QuanLyBenhNhan.html'
    };
    var allowed = allowByRole[role] || [];

    var currentPath = window.location.pathname;
    var m = currentPath.match(/([^/]+)\.html?$/i);
    var currentSlug = m ? m[1] : '';

    // Lọc sidebar
    var nav = document.querySelector('.sidebar .nav');
    if (nav) {
      var links = nav.querySelectorAll("a[href$='.html'], a[href$='.htm']");
      links.forEach(function (a) {
        var href = a.getAttribute('href') || '';
        var mm = href.match(/([^/]+)\.html?$/i);
        var slug = mm ? mm[1] : '';
        var canSee = allowed.some(function (s) { return slug.indexOf(s) !== -1; });
        a.style.display = canSee ? '' : 'none';
      });

      // Ẩn heading trống
      var titles = nav.querySelectorAll('.section-title');
      titles.forEach(function (title) {
        var node = title.nextElementSibling, hasVisible = false;
        while (node && !node.classList.contains('section-title')) {
          if (node.matches && node.matches('a[href]') && node.style.display !== 'none') { hasVisible = true; break; }
          node = node.nextElementSibling;
        }
        title.style.display = hasVisible ? '' : 'none';
      });
    }

    // Chặn truy cập trực tiếp
    var isIndex = /index\.html?$/i.test(currentPath);
    var isAllowedPage = allowed.some(function (s) { return currentSlug.indexOf(s) !== -1; });
    if (!isIndex && !isAllowedPage) {
      var target = defaultPageByRole[role] || '../pages/index.html';
      if (!currentPath.endsWith(target.split('/').pop())) {
        window.location.href = target;
        return;
      }
    }

    // Ẩn/hiện section theo role (data-section)
    var sectionAllowByRole = { 4:['admin','bacsi','thungan','benhnhan'], 3:['bacsi'], 2:['thungan'], 1:['benhnhan'] };
    var allowedSections = sectionAllowByRole[role] || [];
    document.querySelectorAll('[data-section]').forEach(function (sec) {
      var name = sec.getAttribute('data-section');
      sec.style.display = allowedSections.indexOf(name) !== -1 ? '' : 'none';
    });

    // Logout ở mọi trang
    var logoutBtn = document.querySelector('.logout a');
    if (logoutBtn) {
      logoutBtn.addEventListener('click', function (e) {
        e.preventDefault();
        if (confirm('Bạn có chắc muốn đăng xuất không?')) {
          localStorage.removeItem('token');
          localStorage.removeItem('user');
          localStorage.removeItem('role');
          localStorage.removeItem('isLoggedIn');
          window.location.href = '../pages/index.html';
        }
      });
    }
  });
})();
