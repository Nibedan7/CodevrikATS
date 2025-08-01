"use strict";

const ANIMATION_DURATION = 300;

const sidebar = document.getElementById("sidebar");
let mainContentDiv = document.querySelector(".main-content");

const slideHasSub = document.querySelectorAll(".nav > ul > .slide.has-sub");

const firstLevelItems = document.querySelectorAll(
  ".nav > ul > .slide.has-sub > a"
);

const innerLevelItems = document.querySelectorAll(
  ".nav > ul > .slide.has-sub .slide.has-sub > a"
);

class PopperObject {
  instance = null;
  reference = null;
  popperTarget = null;

  constructor(reference, popperTarget) {
    this.init(reference, popperTarget);
  }

  init(reference, popperTarget) {
    this.reference = reference;
    this.popperTarget = popperTarget;
    this.instance = Popper.createPopper(this.reference, this.popperTarget, {
      placement: "bottom",
      strategy: "relative",
      resize: true,
      modifiers: [
        {
          name: "computeStyles",
          options: {
            adaptive: false,
          },
        },
      ],
    });

    document.addEventListener(
      "click",
      (e) => this.clicker(e, this.popperTarget, this.reference),
      false
    );

    const ro = new ResizeObserver(() => {
      this.instance.update();
    });

    ro.observe(this.popperTarget);
    ro.observe(this.reference);
  }

  clicker(event, popperTarget, reference) {
    if (
      sidebar.classList.contains("collapsed") &&
      !popperTarget.contains(event.target) &&
      !reference.contains(event.target)
    ) {
      this.hide();
    }
  }

  hide() {}
}

class Poppers {
  subMenuPoppers = [];

  constructor() {
    this.init();
  }

  init() {
    slideHasSub.forEach((element) => {
      this.subMenuPoppers.push(
        new PopperObject(element, element.lastElementChild)
      );
      this.closePoppers();
    });
  }

  togglePopper(target) {
    if (
      window.getComputedStyle(target).visibility === "hidden" &&
      window.getComputedStyle(target).visibility === undefined
    )
      target.style.visibility = "visible";
    else target.style.visibility = "hidden";
  }

  updatePoppers() {
    this.subMenuPoppers.forEach((element) => {
      element.instance.state.elements.popper.style.display = "none";
      element.instance.update();
    });
  }

  closePoppers() {
    this.subMenuPoppers.forEach((element) => {
      element.hide();
    });
  }
}

const slideUp = (target, duration = ANIMATION_DURATION) => {
  const { parentElement } = target;
  parentElement.classList.remove("open");
  target.style.transitionProperty = "height, margin, padding";
  target.style.transitionDuration = `${duration}ms`;
  target.style.boxSizing = "border-box";
  target.style.height = `${target.offsetHeight}px`;
  target.offsetHeight;
  target.style.overflow = "hidden";
  target.style.height = 0;
  target.style.paddingTop = 0;
  target.style.paddingBottom = 0;
  target.style.marginTop = 0;
  target.style.marginBottom = 0;
  window.setTimeout(() => {
    target.style.display = "none";
    target.style.removeProperty("height");
    target.style.removeProperty("padding-top");
    target.style.removeProperty("padding-bottom");
    target.style.removeProperty("margin-top");
    target.style.removeProperty("margin-bottom");
    target.style.removeProperty("overflow");
    target.style.removeProperty("transition-duration");
    target.style.removeProperty("transition-property");
  }, duration);
};
const slideDown = (target, duration = ANIMATION_DURATION) => {
  const { parentElement } = target;
  parentElement.classList.add("open");
  target.style.removeProperty("display");
  let { display } = window.getComputedStyle(target);
  if (display === "none") display = "block";
  target.style.display = display;
  const height = target.offsetHeight;
  target.style.overflow = "hidden";
  target.style.height = 0;
  target.style.paddingTop = 0;
  target.style.paddingBottom = 0;
  target.style.marginTop = 0;
  target.style.marginBottom = 0;
  target.offsetHeight;
  target.style.boxSizing = "border-box";
  target.style.transitionProperty = "height, margin, padding";
  target.style.transitionDuration = `${duration}ms`;
  target.style.height = `${height}px`;
  target.style.removeProperty("padding-top");
  target.style.removeProperty("padding-bottom");
  target.style.removeProperty("margin-top");
  target.style.removeProperty("margin-bottom");
  window.setTimeout(() => {
    target.style.removeProperty("height");
    target.style.removeProperty("overflow");
    target.style.removeProperty("transition-duration");
    target.style.removeProperty("transition-property");
  }, duration);
};
const slideToggle = (target, duration = ANIMATION_DURATION) => {
  let html = document.querySelector("html");
  if (
    !(
      (html.getAttribute("data-nav-style") === "menu-hover" &&
        html.getAttribute("data-toggled") === "menu-hover-closed" &&
        window.innerWidth >= 992) ||
      (html.getAttribute("data-nav-style") === "icon-hover" &&
        html.getAttribute("data-toggled") === "icon-hover-closed" &&
        window.innerWidth >= 992)
    ) &&
    target &&
    target.nodeType != 3
  ) {
    if (window.getComputedStyle(target).display === "none") {
      return slideDown(target, duration);
    }
    return slideUp(target, duration);
  }
};

const PoppersInstance = new Poppers();

/**
 * wait for the current animation to finish and update poppers position
 */
const updatePoppersTimeout = () => {
  setTimeout(() => {
    PoppersInstance.updatePoppers();
  }, ANIMATION_DURATION);
};

const defaultOpenMenus = document.querySelectorAll(".slide.has-sub.open");

defaultOpenMenus.forEach((element) => {
  element.lastElementChild.style.display = "block";
});

/**
 * handle top level submenu click
 */
firstLevelItems.forEach((element) => {
  element.addEventListener("click", () => {
    let html = document.querySelector("html");
    if (
      !(
        (html.getAttribute("data-nav-style") === "menu-hover" &&
          html.getAttribute("data-toggled") === "menu-hover-closed" &&
          window.innerWidth >= 992) ||
        (html.getAttribute("data-nav-style") === "icon-hover" &&
          html.getAttribute("data-toggled") === "icon-hover-closed" &&
          window.innerWidth >= 992)
      )
    ) {
      const parentMenu = element.closest(".nav.sub-open");
      if (parentMenu)
        parentMenu
          .querySelectorAll(":scope > ul > .slide.has-sub > a")
          .forEach((el) => {
            if (
              el.nextElementSibling.style.display === "block" ||
              window.getComputedStyle(el.nextElementSibling).display === "block"
            ) {
              slideUp(el.nextElementSibling);
            }
          });
      slideToggle(element.nextElementSibling);
    }
  });
});

/**
 * handle inner submenu click
 */
innerLevelItems.forEach((element) => {
  let html = document.querySelector("html");
  element.addEventListener("click", () => {
    const innerMenu = element.closest(".slide-menu");
    if (innerMenu)
      innerMenu.querySelectorAll(":scope .slide.has-sub > a").forEach((el) => {
        if (
          el.nextElementSibling &&
          el.nextElementSibling?.style.display === "block"
        ) {
          slideUp(el.nextElementSibling);
        }
      });
    slideToggle(element.nextElementSibling);
  });
});

/**
 * menu styles
 */

window.addEventListener("resize", () => {
  let mainContent = document.querySelector(".main-content");
  setTimeout(() => {
    if (window.innerWidth <= 992) {
      mainContent.addEventListener("click", menuClose);
    } else {
      mainContent.removeEventListener("click", menuClose);
    }
  }, 100);
});
let headerToggleBtn, WindowPreSize;
(() => {
  let html = document.querySelector("html");
  headerToggleBtn = document.querySelector(".sidemenu-toggle");
  headerToggleBtn.addEventListener("click", toggleSidemenu);
  let mainContent = document.querySelector(".main-content");
  if (window.innerWidth <= 992) {
    mainContent.addEventListener("click", menuClose);
  } else {
    mainContent.removeEventListener("click", menuClose);
  }

  WindowPreSize = [window.innerWidth];
  setNavActive();
  if (
    html.getAttribute("data-nav-layout") === "horizontal" &&
    window.innerWidth >= 992
  ) {
    clearNavDropdown();
    mainContent.addEventListener("click", clearNavDropdown);
  } else {
    mainContent.removeEventListener("click", clearNavDropdown);
  }

  window.addEventListener("resize", ResizeMenu);
  switcherArrowFn();

  if (
    !localStorage.getItem("Ynexlayout") &&
    !localStorage.getItem("Ynexnavstyles") &&
    !localStorage.getItem("Ynexverticalstyles") &&
    !document.querySelector(".landing-body") &&
    document.querySelector("html").getAttribute("data-nav-layout") !==
      "horizontal"
  ) {
    // To enable sidemenu layout styles
    // iconTextFn()
    // detachedFn();
    // closedSidemenuFn();
    // doubletFn();
    if (
      !html.getAttribute("data-vertical-style") &&
      !html.getAttribute("data-nav-style")
    ) {
      iconOverayFn();
    }
  }

  toggleSidemenu();

  if (
    (html.getAttribute("data-nav-style") === "menu-hover" ||
      html.getAttribute("data-nav-style") === "icon-hover") &&
    window.innerWidth >= 992
  ) {
    clearNavDropdown();
  }
  if (window.innerWidth < 992) {
    html.setAttribute("data-toggled", "close");
  }
})();

function ResizeMenu() {
  let html = document.querySelector("html");
  let mainContent = document.querySelector(".main-content");

  WindowPreSize.push(window.innerWidth);
  if (WindowPreSize.length > 2) {
    WindowPreSize.shift();
  }
  if (WindowPreSize.length > 1) {
    if (
      WindowPreSize[WindowPreSize.length - 1] < 992 &&
      WindowPreSize[WindowPreSize.length - 2] >= 992
    ) {
      // less than 992;
      mainContent.addEventListener("click", menuClose);
      setNavActive();
      toggleSidemenu();
      mainContent.removeEventListener("click", clearNavDropdown);
    }

    if (
      WindowPreSize[WindowPreSize.length - 1] >= 992 &&
      WindowPreSize[WindowPreSize.length - 2] < 992
    ) {
      // greater than 992
      mainContent.removeEventListener("click", menuClose);
      toggleSidemenu();
      if (html.getAttribute("data-nav-layout") === "horizontal") {
        clearNavDropdown();
        mainContent.addEventListener("click", clearNavDropdown);
      } else {
        mainContent.removeEventListener("click", clearNavDropdown);
      }
      if (
        !document.querySelector("html").getAttribute("data-toggled") ==
        "double-menu-open"
      ) {
        html.removeAttribute("data-toggled");
      }
    }
  }
  checkHoriMenu();

  if(WindowPreSize[WindowPreSize.length - 1] >= 992 && localStorage.ynexnavstyles == 'menu-click'){
    html.removeAttribute("data-toggled")
  }
}
function menuClose() {
  let html = document.querySelector("html");
  html.setAttribute("data-toggled", "close");
  document.querySelector("#responsive-overlay").classList.remove("active");
}
function toggleSidemenu() {
  let html = document.querySelector("html");
  let sidemenuType = html.getAttribute("data-nav-layout");

  if (window.innerWidth >= 992) {
    if (sidemenuType === "vertical") {
      sidebar.removeEventListener("mouseenter", mouseEntered);
      sidebar.removeEventListener("mouseleave", mouseLeave);
      sidebar.removeEventListener("click", icontextOpen);
      mainContentDiv.removeEventListener("click", icontextClose);
      let sidemenulink = document.querySelectorAll(
        ".main-menu li > .side-menu__item"
      );
      sidemenulink.forEach((ele) =>
        ele.removeEventListener("click", doubleClickFn)
      );

      let verticalStyle = html.getAttribute("data-vertical-style");
      switch (verticalStyle) {
        // closed
        case "closed":
          html.removeAttribute("data-nav-style");
          if (html.getAttribute("data-toggled") === "close-menu-close") {
            html.removeAttribute("data-toggled");
          } else {
            html.setAttribute("data-toggled", "close-menu-close");
          }
          break;
        // icon-overlay
        case "overlay":
          html.removeAttribute("data-nav-style");
          if (html.getAttribute("data-toggled") === "icon-overlay-close") {
            html.removeAttribute("data-toggled", "icon-overlay-close");
            sidebar.removeEventListener("mouseenter", mouseEntered);
            sidebar.removeEventListener("mouseleave", mouseLeave);
          } else {
            if (window.innerWidth >= 992) {
              html.setAttribute("data-toggled", "icon-overlay-close");
              sidebar.addEventListener("mouseenter", mouseEntered);
              sidebar.addEventListener("mouseleave", mouseLeave);
            } else {
              sidebar.removeEventListener("mouseenter", mouseEntered);
              sidebar.removeEventListener("mouseleave", mouseLeave);
            }
          }
          break;
        // icon-text
        case "icontext":
          html.removeAttribute("data-nav-style");
          if (html.getAttribute("data-toggled") === "icon-text-close") {
            html.removeAttribute("data-toggled", "icon-text-close");
            sidebar.removeEventListener("click", icontextOpen);
            mainContentDiv.removeEventListener("click", icontextClose);
          } else {
            html.setAttribute("data-toggled", "icon-text-close");
            if (window.innerWidth >= 992) {
              sidebar.addEventListener("click", icontextOpen);
              mainContentDiv.addEventListener("click", icontextClose);
            } else {
              sidebar.removeEventListener("click", icontextOpen);
              mainContentDiv.removeEventListener("click", icontextClose);
            }
          }
          break;
        // doublemenu
        case "doublemenu":
          html.removeAttribute("data-nav-style");
          if (html.getAttribute("data-toggled") === "double-menu-open") {
            html.setAttribute("data-toggled", "double-menu-close");
            if (document.querySelector(".slide-menu")) {
              let slidemenu = document.querySelectorAll(".slide-menu");
              slidemenu.forEach((e) => {
                if (e.classList.contains("double-menu-active")) {
                  e.classList.remove("double-menu-active");
                }
              });
            }
          } else {
            let sidemenu = document.querySelector(".side-menu__item.active");
            if (sidemenu) {
              html.setAttribute("data-toggled", "double-menu-open");
              if (sidemenu.nextElementSibling) {
                sidemenu.nextElementSibling.classList.add("double-menu-active");
              } else {
                document.querySelector("html").setAttribute("data-toggled", "");
              }
            }
          }

          doublemenu();
          break;
        // detached
        case "detached":
          if (html.getAttribute("data-toggled") === "detached-close") {
            html.removeAttribute("data-toggled", "detached-close");
            sidebar.removeEventListener("mouseenter", mouseEntered);
            sidebar.removeEventListener("mouseleave", mouseLeave);
          } else {
            html.setAttribute("data-toggled", "detached-close");
            if (window.innerWidth >= 992) {
              sidebar.addEventListener("mouseenter", mouseEntered);
              sidebar.addEventListener("mouseleave", mouseLeave);
            } else {
              sidebar.removeEventListener("mouseenter", mouseEntered);
              sidebar.removeEventListener("mouseleave", mouseLeave);
            }
          }
          break;
        // defaultlo
        case "default":
          iconOverayFn();
          html.removeAttribute("data-toggled");
      }
      
      let menuclickStyle = html.getAttribute('data-nav-style');
      switch (menuclickStyle) {
        case 'menu-click':
          if (html.getAttribute('data-toggled') === 'menu-click-closed') {
            html.removeAttribute('data-toggled');
          }
          else {
            html.setAttribute('data-toggled', 'menu-click-closed');
          }
          break;
        case 'menu-hover':
          if (html.getAttribute('data-toggled') === 'menu-hover-closed') {
            html.removeAttribute('data-toggled');
            setNavActive()
          }
          else {
            html.setAttribute('data-toggled', 'menu-hover-closed');
            clearNavDropdown()
          }
          break;
        case 'icon-click':
          if (html.getAttribute('data-toggled') === 'icon-click-closed') {
            html.removeAttribute('data-toggled');
          }
          else {
            html.setAttribute('data-toggled', 'icon-click-closed');
          }
          break;
        case 'icon-hover':
          if (html.getAttribute('data-toggled') === 'icon-hover-closed') {
            html.removeAttribute('data-toggled');
            setNavActive()
          }
          else {
            html.setAttribute('data-toggled', 'icon-hover-closed');
            clearNavDropdown()
          }
          break;
        //for making any horizontal style as default
        default:
      }
    }
  } else {
    if (html.getAttribute("data-toggled") === "close") {
      html.setAttribute("data-toggled", "open");
      let i = document.createElement("div");
      i.id = "responsive-overlay";
      setTimeout(() => {
        if (document.querySelector("html").getAttribute("data-toggled") == "open") {
          document.querySelector("#responsive-overlay").classList.add("active");
          document
            .querySelector("#responsive-overlay")
            .addEventListener("click", () => {
              document
                .querySelector("#responsive-overlay")
                .classList.remove("active");
              menuClose();
            });
        }
        window.addEventListener("resize", () => {
          if (window.screen.width >= 992) {
            document.querySelector("#responsive-overlay").classList.remove("active");
          }
        });
      }, 100);
    } else {
      html.setAttribute("data-toggled", "close");
    }
  }
}
function mouseEntered() {
  let html = document.querySelector("html");
  html.setAttribute("icon-overlay", "open");
}
function mouseLeave() {
  let html = document.querySelector("html");
  html.removeAttribute("icon-overlay");
}
function icontextOpen() {
  let html = document.querySelector("html");
  html.setAttribute("icon-text", "open");
}
function icontextClose() {
  let html = document.querySelector("html");
  html.removeAttribute("icon-text");
}
function closedSidemenuFn() {
  let html = document.querySelector("html");
  html.setAttribute("data-nav-layout", "vertical");
  html.setAttribute("data-vertical-style", "closed");
  toggleSidemenu();
}
function detachedFn() {
  let html = document.querySelector("html");
  html.setAttribute("data-nav-layout", "vertical");
  html.setAttribute("data-vertical-style", "detached");
  toggleSidemenu();
}
function iconTextFn() {
  let html = document.querySelector("html");
  html.setAttribute("data-nav-layout", "vertical");
  html.setAttribute("data-vertical-style", "icontext");
  toggleSidemenu();
}
function iconOverayFn() {
  let html = document.querySelector("html");
  html.setAttribute("data-nav-layout", "vertical");
  html.setAttribute("data-vertical-style", "overlay");
  toggleSidemenu();
}
function doubletFn() {
  let html = document.querySelector("html");
  html.setAttribute("data-nav-layout", "vertical");
  html.setAttribute("data-vertical-style", "doublemenu");
  toggleSidemenu();

  // Select the menu slide item
  const menuSlideItem = document.querySelectorAll(
    ".main-menu > li > .side-menu__item"
  );

  // Create the tooltip element
  const tooltip = document.createElement("div");
  // tooltip.textContent = "This is a tooltip";

  // Set the CSS properties of the tooltip element
  tooltip.style.setProperty("position", "fixed");
  tooltip.style.setProperty("display", "none");
  tooltip.style.setProperty("padding", "0.5rem");
  tooltip.style.setProperty("font-weight", "500");
  tooltip.style.setProperty("font-size", "0.75rem");
  tooltip.style.setProperty("background-color", "rgb(15, 23 ,42)");
  tooltip.style.setProperty("color", "rgb(255, 255 ,255)");
  tooltip.style.setProperty("margin-inline-start", "45px");
  tooltip.style.setProperty("border-radius", "0.25rem");
  tooltip.style.setProperty("z-index", "99");

  menuSlideItem.forEach((e) => {
    // Add an event listener to the menu slide item to show the tooltip
    e.addEventListener("mouseenter", () => {
      tooltip.style.setProperty("display", "block");
      tooltip.textContent = e.querySelector(".side-menu__label").textContent;
      if (
        document.querySelector("html").getAttribute("data-vertical-style") ==
        "doublemenu"
      ) {
        e.appendChild(tooltip);
      }
    });

    // Add an event listener to hide the tooltip
    e.addEventListener("mouseleave", () => {
      tooltip.style.setProperty("display", "none");
      tooltip.textContent = e.querySelector(".side-menu__label").textContent;
      if (
        document.querySelector("html").getAttribute("data-vertical-style") ==
        "doublemenu"
      ) {
        e.removeChild(tooltip);
      }
    });
  });
}
function menuClickFn() {
  let html = document.querySelector('html');
  html.setAttribute('data-nav-style', 'menu-click');
  html.removeAttribute('data-hor-style');
  html.removeAttribute('data-vertical-style');

  toggleSidemenu();
  if (html.getAttribute('data-nav-layout') === 'vertical') {
    setNavActive();
  }
}
function menuhoverFn() {
  let html = document.querySelector('html');
  html.setAttribute('data-nav-style', 'menu-hover');
  html.removeAttribute('data-hor-style');
  html.removeAttribute('data-vertical-style');
  toggleSidemenu();
  if (html.getAttribute('data-toggled') === 'menu-hover-closed') {
    clearNavDropdown();
  }
}
function iconClickFn() {
  let html = document.querySelector('html');
  html.setAttribute('data-nav-style', 'icon-click');
  toggleSidemenu();
  if (html.getAttribute('data-nav-layout') === 'vertical') {
    setNavActive();
  }
}
function iconHoverFn() {
  let html = document.querySelector('html');
  html.setAttribute('data-nav-style', 'icon-hover');
  toggleSidemenu();
  clearNavDropdown();
}
function setNavActive() {
  let currentPath = window.location.pathname.split("/")[0];

  currentPath =
    location.pathname == "/" ? "index.html" : location.pathname.substring(1);
  currentPath = currentPath.substring(currentPath.lastIndexOf("/") + 1);
  let sidemenuItems = document.querySelectorAll(".side-menu__item");
  sidemenuItems.forEach((e) => {
    if (currentPath === "/") {
      currentPath = "index.html";
    }
    if (e.getAttribute("href") === currentPath) {
      e.classList.add("active");
      e.parentElement.classList.add("active");
      let parent = e.closest("ul");
      let parentNotMain = e.closest("ul:not(.main-menu)");
      let hasParent = true;
      while (hasParent) {
        if (parent) {
          parent.classList.add("active");
          parent.previousElementSibling.classList.add("active");
          parent.parentElement.classList.add("active");
          slideDown(parent);
          parent = parent.parentElement.closest("ul");
          //
          if (parent && parent.closest("ul:not(.main-menu)")) {
            parentNotMain = parent.closest("ul:not(.main-menu)");
          }
          if (!parentNotMain) hasParent = false;
        } else {
          hasParent = false;
        }
      }
    }
  });
}
function clearNavDropdown() {
  let sidemenus = document.querySelectorAll("ul.slide-menu");
  sidemenus.forEach((e) => {
    let parent = e.closest("ul");
    let parentNotMain = e.closest("ul:not(.main-menu)");
    if (parent) {
      let hasParent = parent.closest("ul.main-menu");
      while (hasParent) {
        parent.classList.add("active");
        slideUp(parent);
        //
        parent = parent.parentElement.closest("ul");
        parentNotMain = parent.closest("ul:not(.main-menu)");
        if (!parentNotMain) hasParent = false;
      }
    }
  });
}
function switcherArrowFn() {
  // used to remove is-expanded class and remove class on clicking arrow buttons
  function slideClick() {
    let slide = document.querySelectorAll(".slide");
    let slideMenu = document.querySelectorAll(".slide-menu");
    slide.forEach((element, index) => {
      if (element.classList.contains("is-expanded") == true) {
        element.classList.remove("is-expanded");
      }
    });
    slideMenu.forEach((element, index) => {
      if (element.classList.contains("open") == true) {
        element.classList.remove("open");
        element.style.display = "none";
      }
    });
  }

  slideClick();
}
let slideLeft = document.querySelector(".slide-left");
//let slideRight = document.querySelector(".slide-right");
slideLeft.addEventListener("click", () => {
  let menuNav = document.querySelector(".main-menu");
  let mainContainer1 = document.querySelector(".main-sidebar");
  let marginLeftValue = Math.ceil(
    Number(window.getComputedStyle(menuNav).marginLeft.split("px")[0])
  );
  let marginRightValue = Math.ceil(
    Number(window.getComputedStyle(menuNav).marginRight.split("px")[0])
  );
  let mainContainer1Width = mainContainer1.offsetWidth;
  if (menuNav.scrollWidth > mainContainer1.offsetWidth) {
    if (!(document.querySelector("html").getAttribute("dir") === "rtl")) {
      if (
        marginLeftValue < 0 &&
        !(Math.abs(marginLeftValue) < mainContainer1Width)
      ) {
        menuNav.style.marginRight = 0;
        menuNav.style.marginLeft =
          Number(menuNav.style.marginLeft.split("px")[0]) +
          Math.abs(mainContainer1Width) +
          "px";
       // slideRight.classList.remove("hidden");
      } else if (marginLeftValue >= 0) {
        menuNav.style.marginLeft = "0px";
        slideLeft.classList.add("hidden");
      //  slideRight.classList.remove("hidden");
      } else {
        menuNav.style.marginLeft = "0px";
        slideLeft.classList.add("hidden");
        //slideRight.classList.remove("hidden");
      }
    } else {
      if (
        marginRightValue < 0 &&
        !(Math.abs(marginRightValue) < mainContainer1Width)
      ) {
        menuNav.style.marginLeft = 0;
        menuNav.style.marginRight =
          Number(menuNav.style.marginRight.split("px")[0]) +
          Math.abs(mainContainer1Width) +
          "px";
       // slideRight.classList.remove("hidden");
      } else if (marginRightValue >= 0) {
        menuNav.style.marginRight = "0px";
        slideLeft.classList.add("hidden");
        //slideRight.classList.remove("hidden");
      } else {
        menuNav.style.marginRight = "0px";
        slideLeft.classList.add("hidden");
        //slideRight.classList.remove("hidden");
      }
    }
  } else {
    document.querySelector(".main-menu").style.marginLeft = "0px";
    document.querySelector(".main-menu").style.marginRight = "0px";
  }
  let element = document.querySelector(".main-menu > .slide.open")
      let element1 = document.querySelector(".main-menu > .slide.open >ul")
      if(element){
        element.classList.remove("active")
      }
      if(element1){
        element1 .style.display = "none"
      }



  switcherArrowFn();
  return;
  //
});
//slideRight.addEventListener("click", () => {
//  let menuNav = document.querySelector(".main-menu");
//  let mainContainer1 = document.querySelector(".main-sidebar");
//  let marginLeftValue = Math.ceil(
//    Number(window.getComputedStyle(menuNav).marginLeft.split("px")[0])
//  );
//  let marginRightValue = Math.ceil(
//    Number(window.getComputedStyle(menuNav).marginRight.split("px")[0])
//  );
//  let check = menuNav.scrollWidth - mainContainer1.offsetWidth;
//  let mainContainer1Width = mainContainer1.offsetWidth;

//  if (menuNav.scrollWidth > mainContainer1.offsetWidth) {
//    if (!(document.querySelector("html").getAttribute("dir") === "rtl")) {
//      if (Math.abs(check) > Math.abs(marginLeftValue)) {
//        menuNav.style.marginRight = 0;
//        if (
//          !(Math.abs(check) > Math.abs(marginLeftValue) + mainContainer1Width)
//        ) {
//          mainContainer1Width = Math.abs(check) - Math.abs(marginLeftValue);
//          slideRight.classList.add("hidden");
//        }
//        menuNav.style.marginLeft =
//          Number(menuNav.style.marginLeft.split("px")[0]) -
//          Math.abs(mainContainer1Width) +
//          "px";
//        slideLeft.classList.remove("hidden");
//      }
//    } else {
//      if (Math.abs(check) > Math.abs(marginRightValue)) {
//        menuNav.style.marginLeft = 0;
//        if (
//          !(Math.abs(check) > Math.abs(marginRightValue) + mainContainer1Width)
//        ) {
//          mainContainer1Width = Math.abs(check) - Math.abs(marginRightValue);
//          slideRight.classList.add("hidden");
//        }
//        menuNav.style.marginRight =
//          Number(menuNav.style.marginRight.split("px")[0]) -
//          Math.abs(mainContainer1Width) +
//          "px";
//        slideLeft.classList.remove("hidden");
//      }
//    }
//  }
//  let element = document.querySelector(".main-menu > .slide.open")
//      let element1 = document.querySelector(".main-menu > .slide.open >ul")
//      if(element){
//        element.classList.remove("active")
//      }
//      if(element1){
//        element1 .style.display = "none"
//      }



//  switcherArrowFn();
//  return;
//});
function checkHoriMenu() {
  let menuNav = document.querySelector(".main-menu");
  let mainContainer1 = document.querySelector(".main-sidebar");
  let slideLeft = document.querySelector(".slide-left");
  /*let slideRight = document.querySelector(".slide-right");*/
  let marginLeftValue = Math.ceil(
    Number(window.getComputedStyle(menuNav).marginLeft.split("px")[0])
  );
  let marginRightValue = Math.ceil(
    Number(window.getComputedStyle(menuNav).marginRight.split("px")[0])
  );
  let check = menuNav.scrollWidth - mainContainer1.offsetWidth;
  // Show/Hide the arrows
  if (menuNav.scrollWidth > mainContainer1.offsetWidth) {
    //slideRight.classList.remove("hidden");
    slideLeft.classList.add("hidden");
  } else {
    //slideRight.classList.add("hidden");
    slideLeft.classList.add("hidden");
    menuNav.style.marginLeft = "0px";
    menuNav.style.marginRight = "0px";
  }
  if (!(document.querySelector("html").getAttribute("dir") === "rtl")) {
    // LTR check the width and adjust the menu in screen
    if (menuNav.scrollWidth > mainContainer1.offsetWidth) {
      if (Math.abs(check) < Math.abs(marginLeftValue)) {
        menuNav.style.marginLeft = -check + "px";
        slideLeft.classList.remove("hidden");
        //slideRight.classList.add("hidden");
      }
    }
    if (marginLeftValue == 0) {
      slideLeft.classList.add("hidden");
      //slideRight.classList.remove("hidden");
    } else {
      slideLeft.classList.remove("hidden");
    }
  } else {
    // RTL check the width and adjust the menu in screen
    if (menuNav.scrollWidth > mainContainer1.offsetWidth) {
      if (Math.abs(check) < Math.abs(marginRightValue)) {
        menuNav.style.marginRight = -check + "px";
        slideLeft.classList.remove("hidden");
      //  slideRight.classList.add("hidden");
      }
    }
    if (marginRightValue == 0) {
      slideLeft.classList.add("hidden");
    } else {
      slideLeft.classList.remove("hidden");
    }
  }
  if (marginLeftValue != 0 || marginRightValue != 0) {
    slideLeft.classList.remove("hidden");
  }
}

// double-menu click toggle start
function doublemenu() {
  if (window.innerWidth >= 992) {
    let html = document.querySelector("html");
    let sidemenulink = document.querySelectorAll(
      ".main-menu > li > .side-menu__item"
    );
    sidemenulink.forEach((ele) => {
      ele.addEventListener("click", doubleClickFn);
    });
  }
}
function doubleClickFn() {
  var $this = this;
  let html = document.querySelector("html");
  var checkElement = $this.nextElementSibling;
  if (checkElement) {
    if (!checkElement.classList.contains("double-menu-active")) {
      if (document.querySelector(".slide-menu")) {
        let slidemenu = document.querySelectorAll(".slide-menu");
        slidemenu.forEach((e) => {
          if (e.classList.contains("double-menu-active")) {
            e.classList.remove("double-menu-active");
            html.setAttribute("data-toggled", "double-menu-close");
          }
        });
      }
      checkElement.classList.add("double-menu-active");
      html.setAttribute("data-toggled", "double-menu-open");
    }
  }
}
// double-menu click toggle end

window.addEventListener("unload", () => {
  let mainContent = document.querySelector(".main-content");
  mainContent.removeEventListener("click", clearNavDropdown);
  window.removeEventListener("resize", ResizeMenu);
  let sidemenulink = document.querySelectorAll(
    ".main-menu li > .side-menu__item"
  );
  sidemenulink.forEach((ele) =>
    ele.removeEventListener("click", doubleClickFn)
  );
});

let customScrollTop = () => {
  document.querySelectorAll(".side-menu__item").forEach((ele) => {
    if (ele.classList.contains("active")) {
      let elemRect = ele.getBoundingClientRect();
      if (
        ele.children[0] &&
        ele.parentElement.classList.contains("has-sub") &&
        elemRect.top > 435
      ) {
        ele.scrollIntoView({ behavior: "smooth" });
      }
    }
  });
};
setTimeout(() => {
  customScrollTop();
}, 300);

// For Horizontal menu Overflow
// document.querySelectorAll(".side-menu__item").forEach((element) => {
//   element.addEventListener("click", () => {
//     let ulMenu = element.parentNode.querySelector(".child2");
//     if (ulMenu) {
//       const elementRect = ulMenu.getBoundingClientRect();
//       const isOverflowing =
//         elementRect.right > window.innerWidth ||
//         elementRect.bottom > window.innerHeight;
//       if (
//         isOverflowing &&
//         document.querySelector("html").getAttribute("data-nav-layout") ==
//           "horizontal" &&
//         document.querySelector("html").getAttribute("dir") == "ltr"
//       ) {
//         ulMenu.style.setProperty("right", "100%", "important");
//         ulMenu.style.setProperty("left", "auto", "important");
//       }

//       const isOverflowingRTL = ulMenu.scrollWidth <= ulMenu.clientWidth;
//       if (
//         isOverflowingRTL &&
//         ulMenu.scrollWidth != 0 &&
//         document.querySelector("html").getAttribute("data-nav-layout") ==
//           "horizontal" &&
//         document.querySelector("html").getAttribute("dir") == "rtl"
//       ) {
//         ulMenu.style.setProperty("left", "100%", "important");
//         ulMenu.style.setProperty("right", "auto", "important");
//       }
//     }
//   });
// });



// for menu click active close
if(!document.querySelector(".landing-body")){
  document.querySelector(".content").addEventListener("click", () => {
    document.querySelectorAll(".slide-menu").forEach((ele) => {
      if (document.querySelector("html").getAttribute("data-toggled") == 'menu-click-closed' || document.querySelector("html").getAttribute("data-toggled") == 'icon-click-closed') {
        ele.style.display = "none"
      }
    })
  })
}
// for menu click active close