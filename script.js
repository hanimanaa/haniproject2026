// =================================================================
// هذا الملف لم يعد يحتوي على متغيرات ثابتة!
// سيتم قراءة إعدادات GitHub من سمات البيانات في ملف index.html
// =================================================================

// العناصر اللازمة لميزة التمييز النشط (تبقى كما هي)
const sections = document.querySelectorAll('.content section'); 
const navLinks = document.querySelectorAll('.sidebar ul li a'); 

// دالة تحديد القسم النشط (تبقى كما هي)
function highlightActiveLink() {
    let currentSectionId = '';
    const scrollY = window.scrollY; 

    sections.forEach(section => {
        if (scrollY >= section.offsetTop - 100) {
            currentSectionId = section.getAttribute('id');
        }
    });

    navLinks.forEach(a => {
        a.classList.remove('active');
    });

    navLinks.forEach(a => {
        if (a.href.endsWith(currentSectionId)) {
            a.classList.add('active');
        }
    });
}


document.addEventListener('DOMContentLoaded', () => {
    
    const codeBlock = document.getElementById('github-code-block');
    const fileLink = document.getElementById('github-file-link');

    if (codeBlock) {
        // 🌟 الخطوة الجديدة: قراءة البيانات من سمات HTML 🌟
        const GITHUB_USERNAME = codeBlock.dataset.githubUser || "DefaultUser";
        const REPO_NAME = codeBlock.dataset.repoName || "DefaultRepo";
        const FILE_PATH = codeBlock.dataset.filePath;
        const LANGUAGE = codeBlock.dataset.language || "clike"; // لغة التلوين

        if (!FILE_PATH) {
            codeBlock.textContent = "خطأ: لم يتم تحديد مسار الملف (data-file-path) في HTML.";
            return;
        }
        
        // بناء رابط الملف الخام (Raw URL)
        const RAW_FILE_URL = `https://raw.githubusercontent.com/${GITHUB_USERNAME}/${REPO_NAME}/main/${FILE_PATH}`;
        // بناء رابط الملف على واجهة GitHub (للنقر)
        const GITHUB_LINK_URL = `https://github.com/${GITHUB_USERNAME}/${REPO_NAME}/blob/main/${FILE_PATH}`;

        // 1. تحديث رابط "عرض الملف على GitHub"
        if (fileLink) {
            fileLink.href = GITHUB_LINK_URL;
        }
        
        // 2. جلب محتوى الكود من GitHub
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
                
                // 3. تطبيق التلوين (Prism.js)
                if (window.Prism) {
                    // تحديث فئة اللغة في وسم <code> قبل التلوين
                    codeBlock.className = `language-${LANGUAGE}`; 
                    Prism.highlightElement(codeBlock);
                }
            })
            .catch(error => {
                console.error("Failed to fetch code from GitHub:", error);
                codeBlock.textContent = `عفواً، فشل تحميل الكود من المسار: ${FILE_PATH}. تأكد من أن المستودع عام وأن المسار صحيح.`;
            });
    }

    // تفعيل ميزة التمييز النشط
    highlightActiveLink();
    window.addEventListener('scroll', highlightActiveLink);
});