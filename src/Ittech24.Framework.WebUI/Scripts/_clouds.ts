/**
 * Builds moving clouds on a background, this will then be added
 * to the DOM of the webpage
 * 
 * @author Carlos Lapao <cjlapao@ittech24.co.uk>
 * @version 0.5.0.100
 */

class Clouds {
    svgCloudPath: string;

    private _amount: number;
    private _noise: boolean;

    public get amount(): number {
        return this._amount;
    }
    public set amount(theAmount: number) {
        this._amount = theAmount;
    }

    public get noise(): boolean {
        return this._noise;
    }
    public set noise(thisNoise: boolean) {
        this._noise = thisNoise;
    }

    colors: string[];

    private background: string = '#1a75ff';

    constructor(arg?: any) {
        this.colors = ["#FFFFFF", "#F5F5F5", "#E8E8E8", "#DCDCDC", "#D3D3D3"];
        this.svgCloudPath = "d=\"m 27.800094,0.5176531 c -4.508681,8e-4 -8.659763,2.26707 -10.833759,5.9149 -1.300998,-1.23082 -3.01559,-1.91713 -4.797755,-1.92041 -4.4138331,0.0181 -7.4086569,4.26005 -6.989501,7.8766699 C 2.0994322,13.968733 0,17.170473 0,20.882533 c 0,5.28775 4.2571845,9.54455 9.5449268,9.54455 H 38.080454 c 5.287744,0 9.544547,-4.2568 9.544547,-9.54455 0,-4.57491 -3.187135,-8.37613 -7.470845,-9.31845 C 39.93447,5.4081731 34.469479,0.5216331 27.800094,0.5176531 Z\"";
        if (arg) {
            if (arg.amount) {
                this.amount = arg.amount;
            }
            if (arg.noise) {
                this.noise = arg.noise;
            }
            if (arg.background) {
                this.background = arg.background;
            }
        }
        if (this.amount) {
            this.build(this._amount);
        }
    }

    public build(): void;
    public build(amount: number): void;
    public build(): void {
        var arg = arguments[0];
        if (typeof arg != typeof undefined) {
            this.amount = arg;
        }
        else if (typeof this.amount == typeof undefined) {
            this.amount = 8;
        }

        var body = $('body');
        $('#svgContainer').remove();
        var container = $("<div></div");
        var mheight = body.innerHeight();
        var mwidth = body.innerWidth();
        container.addClass("svgContainer");
        if (this.noise) {
            container.css("background-image", "url('/lib/img/noise.png')");
        }
        container.css("position", "absolute");
        container.css("top", "0");
        container.css("left", "0");
        container.css("height", "100%");
        container.css("width", "100%");
        container.css("overflow", "hidden");
        container.css("background-color", this.background);
        var heightSlice = Math.floor(mheight / this.amount);
        for (var i = 0; i < this.amount; i++) {
            var noise = common.getNoise();
            console.log("noise:" + noise)
            var top = Math.floor((heightSlice * i) * noise);
            var halfLeft = Math.floor((mwidth / 2) * noise);
            var left = common.getRandomNumber(halfLeft, false);

            var atime = Math.floor(Math.random() * (30 - 15) + 15);
            var atime = common.getRandomNumber(30, 15, false);
            var opacity = common.getNoise(common.getRandomNumber(1, 0.7))
            var delay = common.getRandomNumber(2, -2);
            var scale = common.getNoise(common.getRandomNumber(1.5, 0.5));
            var color = this.getRandomColor();

            if (color == null)
                color = "#FFFFFF";

            var element = $("<div></div>");
            element.css('position', 'absolute');
            element.css('top', top + 'px');
            element.css('left', left + 'px');
            element.css('color', color);
            element.css('opacity', opacity);
            element.css('animation-delay', delay + 's');
            element.css('transform', 'scale(' + scale + ')');
            element.css('animation', 'moveclouds ' + atime + 's linear infinite');
            element.css('-webkit-animation', 'moveclouds ' + atime + 's  linear infinite');
            element.append(this.buildSvg());
            container.append(element);
        }
        body.append(container);
    }

    private getRandomColor() {
        var random = common.getRandomNumber(this.colors.length -1, false);
        let color = this.colors[random];
        console.log("color " + color + "| rand" + random);
        return color;
    }

    private buildSvg() {
        var svg = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 50.270832 30.427084\" height=\"115\" width=\"190\"> <path fill=\"currentcolor\" " + this.svgCloudPath + " ></path> </svg>";
        return svg;
    }
}