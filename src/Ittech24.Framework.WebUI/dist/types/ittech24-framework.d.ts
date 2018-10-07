declare class FloatButton {
    constructor();
}

declare class Notifications {
    constructor();
    setBar(): void;
}

declare class Clouds {
    svgCloudPath: string;
    private _amount;
    private _noise;
    amount: number;
    noise: boolean;
    colors: string[];
    private background;
    constructor(arg?: any);
    build(): void;
    build(amount: number): void;
    private getRandomColor;
    private buildSvg;
}

declare module common {
    function getRandomNumber(): number;
    function getRandomNumber(max: number): number;
    function getRandomNumber(max: number, float: boolean): number;
    function getRandomNumber(max: any, min: number): number;
    function getRandomNumber(max: any, min: number, float: boolean): number;
    function getNoise(): number;
    function getNoise(value: number): number;
    function getNoise(value: number, float: boolean): number;
    enum themeColorValue {
        SkyBlue = "#305ee8",
        TealGrey = "#a2b9bc",
        GreenLeaf = "#b2ad7f",
        YellowLight = "#feb236",
        CherryTomato = "#E94B3C",
        pureWhite = "#fefefe"
    }
    enum themColor {
        skyBlue = "skyblue",
        tealGrey = "tealgrey",
        greenLeaf = "greenleaf",
        yellowLight = "yellowlight",
        cherryTomato = "cherrytomato",
        pureWhite = "purewhite"
    }
}

/// <reference types="jquery" />
declare enum DropdownPosition {
    down = 0,
    left = 1,
    right = 2,
    top = 3
}
declare class Dropdown {
    items: JQuery<HTMLElement>;
    constructor();
    private collapseAll;
    private onDropdownClick;
}

declare class Helpers {
    name: string;
    constructor();
    getname(): string;
}

declare var sideBar: iSidebar;
declare var dropdownMenus: Dropdown;
declare var floatButton: FloatButton;
declare var initialize: () => void;

declare class iSidebarSubmenu {
    private parent;
    private submenu;
    private submenuBackButton;
    private submenuBackBar;
    constructor();
    private initializeSubmenu;
    animateIn(): void;
    animateOut(): void;
}

/// <reference types="jquery" />
declare var addResizeBar: () => void;
declare class iSidebar {
    menuPusher: JQuery<HTMLElement>;
    resizeBar: JQuery<HTMLElement>;
    private isidebar;
    private containsHeader;
    private headerSection;
    private headerStartHeight;
    private containsFooter;
    private footerSection;
    private footerStartHeight;
    private contentSection;
    private bodySection;
    private containsFlexyContent;
    private flexyContent;
    private ss1;
    private startX;
    private startY;
    private startWidth;
    private startHeight;
    private tSize;
    private submenu;
    expanded: boolean;
    width: number;
    size: number;
    maxSize: number;
    constructor();
    private createPusher;
    private createResizer;
    private createiSideBarSection;
    private createResizeEvents;
    private isExpanded;
    private doClick;
    private doDown;
    private doDrag;
    private stopDrag;
    private expandBody;
    private contractBody;
}

declare class loadingPanel {
    name: string;
    color: string;
    callback: string;
    constructor(args?: any);
    build(): void;
    private doubleFade;
    private foldingSquare;
    private stretch;
    private bounceBall;
    private bounceDots;
    private dotsCircle;
    private cubeGrid;
    private fadingCircle;
}

declare class SvgViewport {
    x: number;
    y: number;
    w: number;
    h: number;
}
declare class Svg {
    cssClass: string;
    viewport: SvgViewport;
    constructor();
}
