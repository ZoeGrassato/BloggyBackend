<template>
    <div id="app">
        <div id="nav">
            <div class="row w-100">
                <div class="col-1">
                    <img class="shapes-sizing" src="./assets/navbar-shapes.png">
                    <router-link to="/">Bloggy</router-link>
                </div>
                <div class="col-2">Hello</div>
            </div>
            <router-link to="/">Home</router-link> |
            <router-link to="/about">About</router-link>
        </div>
        <router-view />
    </div>
</template>

<style>
    #app {
        font-family: Avenir, Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
    }

    #nav {
        height: 126px;
        padding: 0px;
        background: rgba(0, 0, 0, 0.81);
    }

        #nav a {
            font-weight: bold;
            color: #2c3e50;
        }

            #nav a.router-link-exact-active {
                color: #42b983;
            }

    .shapes-sizing {
        height: 126px;
        transform: rotateX(180deg) rotateY(180deg);
        margin-left: -14px;
    }
</style>
