(function () {
        // a counter that flips like an old-school clock.
        // call `new FlipCounter(el, {params})`, where el is a required jQuery
        // object and params (all optionally) are:
        // - direction {"up" or "down", optional} - which way the flip should go
        // - number {int} - an initial value to throw in the dom

        // after initializing, you may call increment(), decrement(), or flip(int),
        // where int is the delta
        window.FlipCounter = class FlipCounter {
                constructor(el, params = {}) {
                        if ((el == null) || !el.length) {
                                return;
                        }
                        if ((params.direction == null) || ((params.direction != null) && (params.direction !== "up" || params.direction !== "down"))) {
                                params.direction = "down";
                        }
                        if (!params.number) {
                                params.number = 0;
                        }

                        // save settings on the FlipCounter object
                        this.duration = 400; // this should be >= $duration in flip_counter.scss
                        this.direction = params.direction;
                        this.number = params.number;
                        this.flipper = el;

                        // add the flipper html components to the wrapper
                        this.addDomElements();

                        // cache dom elements
                        this.bgTop = this.flipper.find(".background_top");
                        this.bgBottom = this.flipper.find(".background_bottom");
                        this.front = this.flipper.find(".front");
                        this.back = this.flipper.find(".back");

                        // set the initial number and direction
                        this.initializeDomElements();
                }

                increment() {
                        return this.flip(1);
                }

                decrement() {
                        return this.flip(-1);
                }

                flip(delta) {
                        this.prepareTransition(delta);
                        this.flipper.addClass("flip");
                        return setTimeout(() => {
                                this.number = this.nextNumber;
                                return this.reset();
                        }, this.duration);
                }


                // prepare the el for a flip by setting the current and next number
                prepareTransition(delta = 1) {
                        // get the next number
                        this.nextNumber = this.number + delta;
                        if (this.nextNumber === -1) {
                                // keep it in bounds
                                this.nextNumber = 9;
                        }
                        if (this.nextNumber === 10) {
                                this.nextNumber = 0;
                        }
                        if (this.direction === "up") {
                                this.bgTop.text(this.number);
                                this.bgBottom.text(this.nextNumber);
                                this.front.text(this.number);
                                return this.back.text(this.nextNumber);
                        } else {
                                this.bgTop.text(this.nextNumber);
                                this.bgBottom.text(this.number);
                                this.front.text(this.number);
                                return this.back.text(this.nextNumber);
                        }
                }


                // set to the current number and remove the flip class without a transition
                reset() {
                        this.setNumber();
                        this.flipper.addClass("no_transition");
                        this.flipper.removeClass("flip");
                        this.flipper.css("width"); // trigger a redraw
                        return this.flipper.removeClass("no_transition");
                }


                // instantly set all dom elements to the current number
                setNumber(val) {
                        if (val) {
                                this.number = val;
                        }
                        this.bgTop.text(this.number);
                        this.bgBottom.text(this.number);
                        this.front.text(this.number);
                        return this.back.text(this.number);
                }


                // set the initial display number and add a direction class
                // (without a transition)
                initializeDomElements() {
                        this.setNumber();
                        this.flipper.addClass(`no_transition flip_${this.direction}`);
                        this.flipper.css("width"); // trigger a redraw
                        return this.flipper.removeClass("no_transition");
                }

                addDomElements() {
                        return this.flipper.empty().append(`<div class="background_top"></div>
                    <div class="background_bottom"></div>
                    <div class="front"></div>
                    <div class="back"></div>`);
                }

        };


}).call(this);
