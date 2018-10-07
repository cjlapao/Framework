/**
 * Builds the loading spinners on a webpage and add it to the DOM of 
 * the webpage.
 * 
 * all animations can be found at http://tobiasahlin.com/spinkit/
 * took the liberty of changing them  a bit
 * 
 * @author Carlos Lapao <cjlapao@ittech24.co.uk>
 * @version 0.5.0.100
 */

class loadingPanel {
    name: string = "double-fade";
    color: string = "skyblue";
    callback: string;

    constructor(args?: any) {
        if (args) {
            if (args.color) {
                this.color;
            }
            if (args.name) {
                this.name = args.name;
            }
        }
        if (this.name) {
            this.build();
        }
    }

    public build() {
        var target = $('body');
        $(".loading-panel").remove();
        var container = $("<div></div>");
        container.addClass("loading-panel");
        container.attr("id", "loadingPanel");
        if (this.name === 'double-fade') {
            this.doubleFade(container);
        }
        else if (this.name === 'rotateplane') {
            this.foldingSquare(container);
        }
        else if (this.name === 'stretch') {
            this.stretch(container);
        }
        else if (this.name === 'bounce-ball') {
            this.bounceBall(container);
        }
        else if (this.name === 'bounce-dots') {
            this.bounceDots(container);
        }
        else if (this.name === 'dot-circle') {
            this.dotsCircle(container);
        }
        else if (this.name === 'cube-grid') {
            this.cubeGrid(container);
        }
        else if (this.name === 'fading-circle') {
            this.fadingCircle(container);
        }
        else {
            this.doubleFade(container);
        }
    }

    private doubleFade(container: JQuery<HTMLElement>) {
        var spinner = $("<div></div>");
        spinner.addClass("spinner");
        spinner.addClass("spinner-" + this.color);
        var child1 = $("<div></div>");
        child1.addClass("double-bounce1");
        var child2 = $("<div></div>");
        child2.addClass("double-bounce2");
        spinner.append(child1);
        spinner.append(child2);
        container.append(spinner);
        $('body').append(container);
    }

    private foldingSquare(container: JQuery<HTMLElement>) {
        var spinner = $("<div></div>");
        spinner.addClass("spinner");
        spinner.addClass("spinner-" + this.color);
        var child1 = $("<div></div>");
        child1.addClass("rotateplane");
        spinner.append(child1);
        container.append(spinner);
        $('body').append(container);
    }

    private stretch(container: JQuery<HTMLElement>) {
        var spinner = $("<div></div>");
        spinner.addClass("spinner");
        spinner.addClass("spinner-stretch");
        spinner.addClass("spinner-" + this.color);
        var child1 = $("<div></div>");
        child1.addClass("rect1");
        spinner.append(child1);
        var child2 = $("<div></div>");
        child2.addClass("rect2");
        spinner.append(child2);
        var child3 = $("<div></div>");
        child3.addClass("rect3");
        spinner.append(child3);
        var child4 = $("<div></div>");
        child4.addClass("rect4");
        spinner.append(child4);
        var child5 = $("<div></div>");
        child5.addClass("rect5");
        spinner.append(child5);
        container.append(spinner);
        $('body').append(container);
    }

    private bounceBall(container: JQuery<HTMLElement>) {
        var spinner = $("<div></div>");
        spinner.addClass("spinner");
        spinner.addClass("spinner-bounce-ball");
        spinner.addClass("spinner-" + this.color);
        var child1 = $("<div></div>");
        child1.addClass("dot1");
        var child2 = $("<div></div>");
        child2.addClass("dot2");
        spinner.append(child1);
        spinner.append(child2);
        container.append(spinner);
        $('body').append(container);
    }

    private bounceDots(container: JQuery<HTMLElement>) {
        var spinner = $("<div></div>");
        spinner.addClass("spinner");
        spinner.addClass("spinner-bounce-dots");
        spinner.addClass("spinner-" + this.color);
        var child1 = $("<div></div>");
        child1.addClass("bounce-dot1");
        var child2 = $("<div></div>");
        child2.addClass("bounce-dot2");
        var child3 = $("<div></div>");
        child3.addClass("bounce-dot3");
        spinner.append(child1);
        spinner.append(child2);
        spinner.append(child3);
        container.append(spinner);
        $('body').append(container);
    }

    private dotsCircle(container: JQuery<HTMLElement>) {
        var spinner = $("<div></div>");
        spinner.addClass("spinner");
        spinner.addClass("spinner-dot-circle");
        spinner.addClass("spinner-" + this.color);
        var child1 = $("<div></div>");
        child1.addClass("dot-circle1");
        child1.addClass("circle-child");
        child1.addClass("spinner-before-color");
        var child2 = $("<div></div>");
        child2.addClass("dot-circle2");
        child2.addClass("circle-child");
        child2.addClass("spinner-before-color");
        var child3 = $("<div></div>");
        child3.addClass("dot-circle3");
        child3.addClass("circle-child");
        child3.addClass("spinner-before-color");
        var child4 = $("<div></div>");
        child4.addClass("dot-circle4");
        child4.addClass("circle-child");
        child4.addClass("spinner-before-color");
        var child5 = $("<div></div>");
        child5.addClass("dot-circle5");
        child5.addClass("circle-child");
        child5.addClass("spinner-before-color");
        var child6 = $("<div></div>");
        child6.addClass("dot-circle6");
        child6.addClass("circle-child");
        child6.addClass("spinner-before-color");
        var child7 = $("<div></div>");
        child7.addClass("dot-circle7");
        child7.addClass("circle-child");
        child7.addClass("spinner-before-color");
        var child8 = $("<div></div>");
        child8.addClass("dot-circle8");
        child8.addClass("circle-child");
        child8.addClass("spinner-before-color");
        var child9 = $("<div></div>");
        child9.addClass("dot-circle9");
        child9.addClass("circle-child");
        child9.addClass("spinner-before-color");
        var child10 = $("<div></div>");
        child10.addClass("dot-circle10");
        child10.addClass("circle-child");
        child10.addClass("spinner-before-color");
        var child11 = $("<div></div>");
        child11.addClass("dot-circle11");
        child11.addClass("circle-child");
        child11.addClass("spinner-before-color");
        var child12 = $("<div></div>");
        child12.addClass("dot-circle12");
        child12.addClass("circle-child");
        child12.addClass("spinner-before-color");
        spinner.append(child1);
        spinner.append(child2);
        spinner.append(child3);
        spinner.append(child4);
        spinner.append(child5);
        spinner.append(child6);
        spinner.append(child7);
        spinner.append(child8);
        spinner.append(child9);
        spinner.append(child10);
        spinner.append(child11);
        spinner.append(child12);
        container.append(spinner);
        $('body').append(container);
    }

    private cubeGrid(container: JQuery<HTMLElement>) {
        var spinner = $("<div></div>");
        spinner.addClass("spinner");
        spinner.addClass("spinner-cube-grid");
        spinner.addClass("spinner-" + this.color);
        var child1 = $("<div></div>");
        child1.addClass("grid-cube");
        child1.addClass("grid-cube1");
        spinner.append(child1);
        var child2 = $("<div></div>");
        child2.addClass("grid-cube");
        child2.addClass("grid-cube2");
        spinner.append(child2);
        var child3 = $("<div></div>");
        child3.addClass("grid-cube");
        child3.addClass("grid-cube3");
        spinner.append(child3);
        var child4 = $("<div></div>");
        child4.addClass("grid-cube");
        child4.addClass("grid-cube4");
        spinner.append(child4);
        var child5 = $("<div></div>");
        child5.addClass("grid-cube");
        child5.addClass("grid-cube5");
        spinner.append(child5);
        var child6 = $("<div></div>");
        child6.addClass("grid-cube");
        child6.addClass("grid-cube6");
        spinner.append(child6);
        var child7 = $("<div></div>");
        child7.addClass("grid-cube");
        child7.addClass("grid-cube7");
        spinner.append(child7);
        var child8 = $("<div></div>");
        child8.addClass("grid-cube");
        child8.addClass("grid-cube8");
        spinner.append(child8);
        var child9 = $("<div></div>");
        child9.addClass("grid-cube");
        child9.addClass("grid-cube9");
        spinner.append(child9);
        container.append(spinner);
        $('body').append(container);
    }

    private fadingCircle(container: JQuery<HTMLElement>) {
        var spinner = $("<div></div>");
        spinner.addClass("spinner");
        spinner.addClass("spinner-fading-circle");
        spinner.addClass("spinner-" + this.color);
        var child1 = $("<div></div>");
        child1.addClass("fading-circle1");
        child1.addClass("fading-circle");
        child1.addClass("spinner-before-color");
        var child2 = $("<div></div>");
        child2.addClass("fading-circle2");
        child2.addClass("fading-circle");
        child2.addClass("spinner-before-color");
        var child3 = $("<div></div>");
        child3.addClass("fading-circle3");
        child3.addClass("fading-circle");
        child3.addClass("spinner-before-color");
        var child4 = $("<div></div>");
        child4.addClass("fading-circle4");
        child4.addClass("fading-circle");
        child4.addClass("spinner-before-color");
        var child5 = $("<div></div>");
        child5.addClass("fading-circle5");
        child5.addClass("fading-circle");
        child5.addClass("spinner-before-color");
        var child6 = $("<div></div>");
        child6.addClass("fading-circle6");
        child6.addClass("fading-circle");
        child6.addClass("spinner-before-color");
        var child7 = $("<div></div>");
        child7.addClass("fading-circle7");
        child7.addClass("fading-circle");
        child7.addClass("spinner-before-color");
        var child8 = $("<div></div>");
        child8.addClass("fading-circle8");
        child8.addClass("fading-circle");
        child8.addClass("spinner-before-color");
        var child9 = $("<div></div>");
        child9.addClass("fading-circle9");
        child9.addClass("fading-circle");
        child9.addClass("spinner-before-color");
        var child10 = $("<div></div>");
        child10.addClass("fading-circle10");
        child10.addClass("fading-circle");
        child10.addClass("spinner-before-color");
        var child11 = $("<div></div>");
        child11.addClass("fading-circle11");
        child11.addClass("fading-circle");
        child11.addClass("spinner-before-color");
        var child12 = $("<div></div>");
        child12.addClass("fading-circle12");
        child12.addClass("fading-circle");
        child12.addClass("spinner-before-color");
        spinner.append(child1);
        spinner.append(child2);
        spinner.append(child3);
        spinner.append(child4);
        spinner.append(child5);
        spinner.append(child6);
        spinner.append(child7);
        spinner.append(child8);
        spinner.append(child9);
        spinner.append(child10);
        spinner.append(child11);
        spinner.append(child12);
        container.append(spinner);
        $('body').append(container);
    }
}