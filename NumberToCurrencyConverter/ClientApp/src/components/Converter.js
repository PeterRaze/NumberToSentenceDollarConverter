import React, { Component } from 'react';

export class Converter extends Component {
  static displayName = Converter.name;

  constructor(props) {
    super(props);
    this.state = { currency: "", input: "", resultTextColor: "black"};
    this.handleChange = this.handleChange.bind(this);
  }

  convert() {
    this.getConvertedInput();
  }

  handleChange(event) {
    this.setState({
      input: event.target.value
    });
  }

  render() {
    return (
      
      <div className="px-4 py-4 text-center">
        <h1 className="display-5 fw-bold text-body-emphasis">Number To Currency Converter</h1>
        <br/>
        <h4 style={{whiteSpace: "pre-line"}}>Convert a number to dollar currency.<br/>Use comma (,) to include cents up to two decimal places.</h4>
        <br/>
        <br/>
        <h3>Enter the number:</h3>
        <div className="d-grid gap-4">
          <div className="input-group mx-auto" style={{width: "225px"}}>
              <input type="text" className="form-control form-control-lg text-center" value={this.state.input} onChange={this.handleChange}/>
          </div>
          <div className="d-sm-flex justify-content-sm-center">
            <button className="btn btn-primary btn-lg px-4" onClick={() => this.convert()}>Convert</button>
          </div>
            <div className="card mx-auto text-center bg-light mb-3" style={{width: "auto", minWidth: "25%", minHeight: "60px" }}>
              <div className="card-body">
                <h5 className="card-text" style={{ color:this.state.resultTextColor}}>{this.state.currency}</h5>
              </div>
            </div>
        </div>
    </div>
      
    );
  }

  async getConvertedInput() {
    if (this.state.input && !(this.state.input.trim().length === 0))
    {
      const response = await fetch('api/converter/?input=' + this.state.input);
      const data = await response.json();
      let result, textColor; 
      if (response.status === 200)
      {
        result = data.wordRepresentation;
        textColor = "black";
      }
      else if (response.status === 422)
      {
        result = data.detail;
        textColor = "red";
      }
      else
      {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      this.setState({ 
        currency: result,
        resultTextColor: textColor
      });
    }
    else
    {
      this.setState({ 
        currency: "Please insert a value",
        resultTextColor: "red"
      });
    }
  }
}
