class Login extends React.Component {
    constructor() {
        super();
        this.state = {
            value: '',
            error: '',
            teamList: []
        };
    }
    
    

    componentDidMount() {
        this.getTeamList();
        $("#PostTips").hide();
        $("#login-control").validate();
    }

    getTeamList() {
        const _this = this;
        axios.get('home/teams')
            .then(function (response) {
                _this.setState({
                    teamList: response.data
                });
            });
    }

    submit() {
        _userName = $("#UserName").val().trim();
        _password = $("#Password").val().trim();
        if (_userName === '' || _password === '') {  
            $("#PostTips").show();
            $("#PostTips").append(`<li>Username and password are required. </li>`);
            return;
        }
        $("#PostTips").empty();
        var team = $("#Team option:selected").val();
        var user = {
            userName: $("#UserName").val(),
            password: $("#Password").val(),
            team: team,
            rememberMe: $("#RememberMe").checked?0 :1
        };
        axios.post('Home/Login', user)
            .then((res) => {
                if (res.data.result === "ok") {
                    localStorage.setItem("userName", user.userName);
                    window.location.href = "/Home/Main";
                } else if (res.data.result === "failed") {
                    $("#PostTips").show();                    
                    res.data.errorMessage.map(
                        (item, i) => {
                            if (item.EMessage !== null) {
                                $("#PostTips").append(`<li key=${i}>${item.EMessage} </li>`);
                            }
                        }
                    );
                    
                } else {
                    $("#PostTips").show();
                    alert("Unexpected error occured");
                }
                console.log(res);
            });
        
    }

    render() {
        return (
            <div>
            <section id="login-control">
                <img id="logo" src="../../content/imgs/logo.jpg"  />
                <fieldset id="login-field">
                    <h2 className="h2" id="welcomeTitle">Welcome to Promapp</h2>
                    <ul>
                        <li> <input id="UserName" key="UserName" name="UserName" placeholder="Username" type="text" defaultValue="" required /> </li>
                        <li> <input id="Password" key="Password" name="Password" placeholder="Password" type="password" defaultValue="" required /> </li>
                        <li>
                            <select id="Team" key="Team" name="Team" placeholder="Team" defaultValue="" onClick={() => this.getTeamList()}>
                                {this.state.teamList.map(
                                    (item,i) => {
                                        return (
                                            <option key={item.teamId} value={item.TeamName}>
                                                {item.TeamName}
                                            </option>
                                            );
                                    }
                                )}
                            </select>
                            <br />
                            <a href="#forgotten-password">Forgot username or password? Click here.</a>
                        </li>
                    </ul>
                    <input className="btn btn-block btn-primary" type="submit" name="Login" value="Login" onClick={()=>this.submit()} />
                    <div id="PostTips"> </div>
                    <input id="RememberMe" name="RememberMe" type="checkbox" value="false" />
                    <label htmlFor="RememberMe"> Remember Me </label>
                    <div className="validation-summary-valid" > </div>
                </fieldset>
             </section>
             </div>
            );
    }
}

ReactDOM.render(<Login />, document.getElementById("content"));