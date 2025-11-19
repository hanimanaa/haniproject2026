// ** íÑÌì ÇÓÊÈÏÇá åĞå ÇáãÊÛíÑÇÊ ÇáËáÇËÉ (USERNAME, REPO_NAME, FILE_PATH) ÈãÚáæãÇÊ ãÔÑæÚß ÇáÍŞíŞíÉ **

const GITHUB_USERNAME = "AhmedDev"; // ÇÓã ãÓÊÎÏãß İí GitHub
const REPO_NAME = "Final-Project-Recommendation-System"; // ÇÓã ãÓÊæÏÚ ÇáãÔÑæÚ
const FILE_PATH = "src/model/Recommender.py"; // ÇáãÓÇÑ ÇáßÇãá ááãáİ ÏÇÎá ÇáãÓÊæÏÚ (ãËÇá)

// ÈäÇÁ ÑÇÈØ Çáãáİ ÇáÎÇã (Raw URL)
const RAW_FILE_URL = `https://raw.githubusercontent.com/${GITHUB_USERNAME}/${REPO_NAME}/main/${FILE_PATH}`;
// ÈäÇÁ ÑÇÈØ Çáãáİ Úáì æÇÌåÉ GitHub (ááäŞÑ)
const GITHUB_LINK_URL = `https://github.com/${GITHUB_USERNAME}/${REPO_NAME}/blob/main/${FILE_PATH}`;


// ** ÇáÚäÇÕÑ ÇááÇÒãÉ áãíÒÉ ÇáÊãííÒ ÇáäÔØ **
const sections = document.querySelectorAll('.content section'); // ÌãíÚ ÇáÃŞÓÇã
const navLinks = document.querySelectorAll('.sidebar ul li a'); // ÌãíÚ ÇáÑæÇÈØ

// ÏÇáÉ ÊÍÏíÏ ÇáŞÓã ÇáäÔØ æÊãííÒ ÇáÑÇÈØ ÇáãŞÇÈá áå
function highlightActiveLink() {
    let currentSectionId = '';
    const scrollY = window.scrollY; // ãæÖÚ ÇáÊãÑíÑ ÇáÍÇáí

    // ÊßÑÇÑ Úáì ÇáÃŞÓÇã áÊÍÏíÏ ÇáŞÓã ÇáĞí íÙåÑ İí ãäØŞÉ ÇáÚÑÖ
    sections.forEach(section => {
        // äÓÊÎÏã -100px áÅÖÇİÉ ãÓÇİÉ ááÃãÇä ÚäÏ ÇáÊãÑíÑ
        if (scrollY >= section.offsetTop - 100) {
            currentSectionId = section.getAttribute('id');
        }
    });

    // ÅÒÇáÉ ÇáİÆÉ ÇáäÔØÉ ãä ÌãíÚ ÇáÑæÇÈØ
    navLinks.forEach(a => {
        a.classList.remove('active');
    });

    // ÅÖÇİÉ ÇáİÆÉ ÇáäÔØÉ ááÑÇÈØ ÇáãØÇÈŞ áÜ currentSectionId
    navLinks.forEach(a => {
        // äŞÇÑä Èíä äåÇíÉ ÇáÑÇÈØ (ãËá #introduction) æÇáãÚÑøİ ÇáÍÇáí
        if (a.href.endsWith(currentSectionId)) {
            a.classList.add('active');
        }
    });
}


document.addEventListener('DOMContentLoaded', () => {
    const codeBlock = document.getElementById('github-code-block');
    const fileLink = document.getElementById('github-file-link');

    // 1. ÊÍÏíË ÑÇÈØ "ÚÑÖ Çáãáİ Úáì GitHub"
    if (fileLink) {
        fileLink.href = GITHUB_LINK_URL;
    }

    // 2. ÌáÈ ãÍÊæì ÇáßæÏ ãä GitHub
    if (codeBlock) {
        fetch(RAW_FILE_URL)
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.text();
            })
            .then(codeContent => {
                codeBlock.textContent = codeContent;
            })
            .catch(error => {
                console.error("Failed to fetch code from GitHub:", error);
                codeBlock.textContent = `ÚİæÇğ¡ İÔá ÊÍãíá ÇáßæÏ. ÊÃßÏ ãä Ãä ÇáãÓÊæÏÚ ÚÇã æÃä ÇáãÓÇÑ (${FILE_PATH}) ÕÍíÍ.`;
            });
    }

    // 3. ÊİÚíá ãíÒÉ ÇáÊãííÒ ÇáäÔØ
    // ÊÔÛíá ÇáÏÇáÉ ÚäÏ ÊÍãíá ÇáÕİÍÉ
    highlightActiveLink();

    // ÊÔÛíá ÇáÏÇáÉ ßáãÇ ŞÇã ÇáãÓÊÎÏã ÈÇáÊãÑíÑ
    window.addEventListener('scroll', highlightActiveLink);
});