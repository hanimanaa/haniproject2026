

// =================================================================
//                 إعدادات جلب الكود من GitHub
// =================================================================
// 🚨 يرجى استبدال هذه المتغيرات الثلاثة (USERNAME, REPO_NAME, FILE_PATH) بمعلومات مشروعك الحقيقية 🚨

const GITHUB_USERNAME = "hanimanaa"; // اسم مستخدمك في GitHub
const REPO_NAME = "haniproject2026"; // اسم مستودع المشروع
const FILE_PATH = "Model/Product.cs"; // المسار الكامل للملف داخل المستودع (مثال)

// بناء رابط الملف الخام (Raw URL)
const RAW_FILE_URL = `https://raw.githubusercontent.com/${GITHUB_USERNAME}/${REPO_NAME}/main/${FILE_PATH}`;
// بناء رابط الملف على واجهة GitHub (للنقر)
const GITHUB_LINK_URL = `https://github.com/${GITHUB_USERNAME}/${REPO_NAME}/blob/main/${FILE_PATH}`;


// =================================================================
//                 منطق تمييز الرابط النشط
// =================================================================
const sections = document.querySelectorAll('.content section'); // جميع الأقسام
const navLinks = document.querySelectorAll('.sidebar ul li a'); // جميع الروابط

// دالة تحديد القسم النشط وتمييز الرابط المقابل له
function highlightActiveLink() {
    let currentSectionId = '';
    const scrollY = window.scrollY; // موضع التمرير الحالي

    // تكرار على الأقسام لتحديد القسم الذي يظهر في منطقة العرض
    sections.forEach(section => {
        // نستخدم -100px لإضافة مسافة للأمان عند التمرير (Offset)
        if (scrollY >= section.offsetTop - 100) {
            currentSectionId = section.getAttribute('id');
        }
    });

    // إزالة الفئة النشطة من جميع الروابط
    navLinks.forEach(a => {
        a.classList.remove('active');
    });

    // إضافة الفئة النشطة للرابط المطابق لـ currentSectionId
    navLinks.forEach(a => {
        // نقارن بين نهاية الرابط (مثل #introduction) والمعرّف الحالي
        if (a.href.endsWith(currentSectionId)) {
            a.classList.add('active');
        }
    });
}


// =================================================================
//                 تنفيذ الدوال عند تحميل الصفحة
// =================================================================
document.addEventListener('DOMContentLoaded', () => {
    // ⚠️ ملاحظة: نحن نستهدف الآن وسم <code> داخل وسم <pre>
    const codeBlock = document.getElementById('github-code-block');
    const fileLink = document.getElementById('github-file-link');

    // 1. تحديث رابط "عرض الملف على GitHub"
    if (fileLink) {
        fileLink.href = GITHUB_LINK_URL;
    }

    // 2. جلب محتوى الكود من GitHub
    if (codeBlock) {
        fetch(RAW_FILE_URL)
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.text();
            })
            .then(codeContent => {
                // وضع الكود الخام
                codeBlock.textContent = codeContent;
                
                // 🌟 تطبيق التلوين (Prism.js) 🌟
                if (window.Prism) {
                     // هذه الدالة تخبر Prism.js بتطبيق التلوين على المحتوى الجديد
                    Prism.highlightElement(codeBlock);
                }
            })
            .catch(error => {
                console.error("Failed to fetch code from GitHub:", error);
                codeBlock.textContent = `عفواً، فشل تحميل الكود. تأكد من أن المستودع عام وأن المسار (${FILE_PATH}) صحيح.`;
            });
    }

    // 3. تفعيل ميزة التمييز النشط
    // تشغيل الدالة عند تحميل الصفحة
    highlightActiveLink();
    
    // تشغيل الدالة كلما قام المستخدم بالتمرير
    window.addEventListener('scroll', highlightActiveLink);
});