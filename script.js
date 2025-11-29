// =================================================================
//                 منطق تمييز الرابط النشط
// =================================================================
const sections = document.querySelectorAll('.content section'); // جميع الأقسام
// 🌟 تحديث: استهداف روابط الـ top-navbar بدلاً من الشريط الجانبي 🌟
const navLinks = document.querySelectorAll('.top-navbar ul.nav-links a'); // جميع الروابط في القائمة العلوية


// دالة تحديد القسم النشط وتمييز الرابط المقابل له
function highlightActiveLink() {
    // نجعل القسم الأول (introduction) هو القيمة الافتراضية
    let currentSectionId = sections.length > 0 ? sections[0].getAttribute('id') : ''; 
    const scrollY = window.scrollY; 

    // تحديد القسم الذي يظهر في منطقة العرض
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
    
    // إغلاق جميع القوائم المنسدلة بشكل افتراضي لمنع بقائها مفتوحة عند التمرير
    // ⚠️ الإبقاء على هذا الجزء مهم لضمان عدم بقاء القائمة مفتوحة بشكل خاطئ ⚠️
    document.querySelectorAll('.dropdown-menu').forEach(menu => {
         menu.classList.remove('open');
    });

    // إضافة الفئة النشطة للرابط المطابق لـ currentSectionId
    navLinks.forEach(a => {
        // نقارن بين نهاية الرابط (مثل #introduction) والمعرّف الحالي
        if (a.href.endsWith(currentSectionId) && currentSectionId !== '') {
            a.classList.add('active');
            
            // ❌ تم حذف هذا الجزء: لا تفتح القائمة المنسدلة عند التمرير ❌
            // const parentDropdownMenu = a.closest('.dropdown-menu');
            // if (parentDropdownMenu) {
            //      parentDropdownMenu.classList.add('open');
            // }
        }
    });
}


// =================================================================
//                 منطق جلب الكود من GitHub
// =================================================================

document.addEventListener('DOMContentLoaded', () => {
    
    const codeBlocks = document.querySelectorAll('.github-code-block');
    const fileLinks = document.querySelectorAll('.github-file-link'); 

    // 1. تفعيل ميزة التمييز النشط
    highlightActiveLink();
    window.addEventListener('scroll', highlightActiveLink);
    
    
    // 2. معالجة كل كتلة كود (ملف) بشكل مستقل
    codeBlocks.forEach((codeBlock, index) => {
        
        const GITHUB_USERNAME = codeBlock.dataset.githubUser || "hanimanaa";
        const REPO_NAME = codeBlock.dataset.repoName || "haniproject2026";
        const FILE_PATH = codeBlock.dataset.filePath;
        const LANGUAGE = codeBlock.dataset.language || "csharp"; 

        if (!FILE_PATH) {
            codeBlock.textContent = "خطأ: لم يتم تحديد مسار الملف (data-file-path).";
            return;
        }
        
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
                codeBlock.textContent = codeContent;
                
                if (window.Prism) {
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
    // 3. منطق القائمة المنسدلة (Dropdown)
    // =================================================================
    // 🌟 تحديث: استهداف الـ dropdown-toggle 🌟
    const dropdownToggles = document.querySelectorAll('.dropdown-toggle');

    dropdownToggles.forEach(toggle => {
        toggle.addEventListener('click', function(e) {
            e.preventDefault(); 
            
            // تحديد القائمة المنسدلة المجاورة
            const dropdownMenu = this.nextElementSibling;

            // إغلاق أي قائمة منسدلة أخرى مفتوحة
            document.querySelectorAll('.dropdown-menu.open').forEach(openMenu => {
                // نغلق القوائم الأخرى ما لم تكن هي نفسها القائمة الحالية
                if (openMenu !== dropdownMenu) {
                    openMenu.classList.remove('open');
                }
            });

            // فتح أو إغلاق القائمة المنسدلة الحالية
            dropdownMenu.classList.toggle('open');
            this.classList.toggle('active');
        });
    });
});