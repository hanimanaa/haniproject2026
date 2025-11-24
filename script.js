// =================================================================
//                 منطق تمييز الرابط النشط
// =================================================================
const sections = document.querySelectorAll('.content section'); // جميع الأقسام
const navLinks = document.querySelectorAll('.sidebar ul li a'); // جميع الروابط

// دالة تحديد القسم النشط وتمييز الرابط المقابل له
function highlightActiveLink() {
    // 🌟 إصلاح المشكلة: نجعل القسم الأول (introduction) هو القيمة الافتراضية
    let currentSectionId = sections.length > 0 ? sections[0].getAttribute('id') : ''; 
    const scrollY = window.scrollY; 

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
//                 منطق جلب الكود من GitHub
// =================================================================

document.addEventListener('DOMContentLoaded', () => {
    
    // استخدام querySelectorAll للفئة لمعالجة الملفات المتعددة
    const codeBlocks = document.querySelectorAll('.github-code-block');
    const fileLinks = document.querySelectorAll('.github-file-link'); // جميع روابط "عرض الملف على GitHub"

    // 1. تفعيل ميزة التمييز النشط
    highlightActiveLink();
    window.addEventListener('scroll', highlightActiveLink);
    
    
    // 2. معالجة كل كتلة كود (ملف) بشكل مستقل
    codeBlocks.forEach((codeBlock, index) => {
        
        // قراءة البيانات من سمات HTML الخاصة بهذه الكتلة
        const GITHUB_USERNAME = codeBlock.dataset.githubUser || "DefaultUser";
        const REPO_NAME = codeBlock.dataset.repoName || "DefaultRepo";
        const FILE_PATH = codeBlock.dataset.filePath;
        const LANGUAGE = codeBlock.dataset.language || "clike"; // لغة التلوين

        if (!FILE_PATH) {
            codeBlock.textContent = "خطأ: لم يتم تحديد مسار الملف (data-file-path).";
            return;
        }
        
        // بناء رابط الملف الخام (Raw URL) ورابط GitHub
        const RAW_FILE_URL = `https://raw.githubusercontent.com/${GITHUB_USERNAME}/${REPO_NAME}/main/${FILE_PATH}`;
        const GITHUB_LINK_URL = `https://github.com/${GITHUB_USERNAME}/${REPO_NAME}/blob/main/${FILE_PATH}`;

        // تحديث رابط "عرض الملف على GitHub" المقابل
        if (fileLinks[index]) {
            fileLinks[index].href = GITHUB_LINK_URL;
        }
        
        // جلب محتوى الكود من GitHub
        fetch(RAW_FILE_URL)
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status} for ${FILE_PATH}`);
                }
                return response.text();
            })
            .then(codeContent => {
                // وضع الكود الخام
                codeBlock.textContent = codeContent;
                
                // تطبيق التلوين (Prism.js)
                if (window.Prism) {
                    // تحديث فئة اللغة لضمان التلوين الصحيح
                    codeBlock.className = `github-code-block language-${LANGUAGE}`; 
                    Prism.highlightElement(codeBlock);
                }
            })
            .catch(error => {
                console.error("Failed to fetch code from GitHub:", error);
                codeBlock.textContent = `عفواً، فشل تحميل الكود من الملف: ${FILE_PATH}.`;
            });
    });

    // =================================================================
    // 3. منطق القائمة الفرعية (Submenu)
    // =================================================================
    const submenuToggles = document.querySelectorAll('.submenu-toggle');

    submenuToggles.forEach(toggle => {
        toggle.addEventListener('click', function(e) {
            e.preventDefault(); // منع الذهاب إلى href="#"
            
            // تحديد القائمة الفرعية المجاورة
            const submenu = this.nextElementSibling;

            // إغلاق أي قائمة فرعية أخرى مفتوحة
            document.querySelectorAll('.submenu.open').forEach(openMenu => {
                // نغلق القوائم الأخرى ما لم تكن هي نفسها القائمة الحالية
                if (openMenu !== submenu) {
                    openMenu.classList.remove('open');
                }
            });

            // فتح أو إغلاق القائمة الفرعية الحالية
            submenu.classList.toggle('open');
            this.classList.toggle('active');
        });
    });
});