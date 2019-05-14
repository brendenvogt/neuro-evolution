import React, { Component } from 'react';

export class Sim extends Component {
    static displayName = Sim.name;

    constructor (props) {
        super(props);
        this.state = { speed: 60 };
        Sim.mountComponent("Neuroevolution.js");
        Sim.mountComponent("game.js");
    }
    
    setSpeed(speed) { 
        this.state.speed = speed;
    };
    
    static mountComponent(component) {
        const script = document.createElement("script");

        script.type = "text/javascript";
        script.src = component;

        document.body.appendChild(script);
        return script;
    };

    componentDidMount() {
        const canvas = this.refs.canvas;
        var ne = Sim.mountComponent("Neuroevolution.js");
        var game = Sim.mountComponent("game.js");
    }
    
    render () {
        return (
            <div>
                <canvas id="flappy" width="500" height="512"/>
                <br/>
                <button onClick={() => this.setSpeed(60)}>x1</button>
                <button onClick={() => this.setSpeed(120)}>x2</button>
                <button onClick={() => this.setSpeed(180)}>x3</button>
                <button onClick={() => this.setSpeed(300)}>x5</button>
                <button onClick={() => this.setSpeed(0)}>MAX</button>
                <br/>
            </div>
        );
    }
}
