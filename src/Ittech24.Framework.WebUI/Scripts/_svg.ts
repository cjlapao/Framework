class SvgViewport {
    x: number;
    y: number;
    w: number;
    h: number;

}

class Svg {
    cssClass: string;
    viewport: SvgViewport;

    constructor() {
        this.cssClass = "test";
    }
}