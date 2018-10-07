/**
 * Common javascript functions to use on the framework
 * to the DOM of the webpage
 *
 * @author Carlos Lapao <cjlapao@ittech24.co.uk>
 * @version 0.6.0.100
 */

/// <reference path="../node_modules/@types/jquery/index.d.ts" />

module common {
    export function getRandomNumber(): number;
    export function getRandomNumber(max: number): number;
    export function getRandomNumber(max: number, float: boolean): number;
    export function getRandomNumber(max, min: number): number;
    export function getRandomNumber(max, min: number, float: boolean): number;
    export function getRandomNumber(): number {
        var max, min, rand: number;
        var float: boolean;

        switch (arguments.length) {
            case 0:
                rand = Math.random();
                break;
            case 1:
                for (var i = 0; i < arguments.length; i++) {
                    if (typeof arguments[i] == "number") {
                        max = arguments[i];
                    }
                    if (typeof arguments[i] == "boolean") {
                        float = arguments[i];
                    }
                }
                if (max != null) {
                    rand = (Math.random() * max);
                }
                if (float != null && float == false) {
                    rand = Math.floor(Math.random());
                }
                if (float != null && float == true) {
                    rand = Math.random();
                }
                break;
            case 2:
                for (var i = 0; i < arguments.length; i++) {
                    if (typeof arguments[i] == "number") {
                        if (max == null) {
                            max = arguments[i];
                        }
                        else if (min == null) {
                            min = arguments[i];
                        }
                    }
                    if (typeof arguments[i] == "boolean") {
                        float = arguments[i];
                    }
                }
                if (max != null && min == null) {
                    rand = (Math.random() * max);
                }
                if (max != null && min != null) {
                    rand = (Math.random() * (max - min)) + min;
                }
                if (float != null && float == false) {
                    rand = Math.floor(rand);
                }
                break;
            case 3:
                for (var i = 0; i < arguments.length; i++) {
                    if (typeof arguments[i] == "number") {
                        if (max == null) {
                            max = arguments[i];
                        }
                        else if (min == null) {
                            min = arguments[i];
                        }
                    }
                    if (typeof arguments[i] == "boolean") {
                        float = arguments[i];
                    }
                }
                if (max != null && min == null) {
                    rand = (Math.random() * max);
                }
                if (max != null && min != null) {
                    rand = (Math.random() * (max - min)) + min;
                }
                if (float != null && float == false) {
                    rand = Math.floor(rand);
                }
                break;
        }
        return rand;
    }

    export function getNoise(): number;
    export function getNoise(value: number): number;
    export function getNoise(value: number, float: boolean): number;
    export function getNoise(): number {
        var value,
            noise,
            response: number;
        var float: boolean;
        noise = common.getRandomNumber(1.10, 0.9);
        for (var i = 0; i < arguments.length; i++) {
            if (typeof arguments[i] == "number") {
                value = arguments[i];
            }
            if (typeof arguments[i] == "boolean") {
                float = arguments[i];
            }
        }
        if (value != null && float != null && float == false) {
            response = value * noise;
            return Math.floor(response);
        }
        else if (value != null) {
            response = value * noise;
            return response;
        }
        return noise;
    }

    export enum themeColorValue {
        SkyBlue = "#305ee8",
        TealGrey = "#a2b9bc",
        GreenLeaf = "#b2ad7f",
        YellowLight = "#feb236",
        CherryTomato = "#E94B3C",
        pureWhite = "#fefefe"
    }

    export enum themColor {
        skyBlue = "skyblue",
        tealGrey = "tealgrey",
        greenLeaf = "greenleaf",
        yellowLight = "yellowlight",
        cherryTomato = "cherrytomato",
        pureWhite = "purewhite"
    }
}
