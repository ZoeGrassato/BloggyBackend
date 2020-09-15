<template>
    <div id="app">
        <div id="nav">
            <div class="row w-100">
                <div class="col-2">
                    <div class="stacked-parent" style="width: 220px">
                        <img class="shapes-sizing stacked-child" src="./assets/background-image.png">
                        <router-link class="bloggy-link-dimensions color-white font-family-oswald font-size navlink-hover stacked-child" to="/">Bloggy</router-link>
                    </div>
                </div>
                <div class="col-4 col-md-3 col-lg-2">
                    <div class="row w-100 h-50">
                    </div>
                    <div class="row">
                        <router-link class="pl-4 color-white font-family-oswald font-size navlink-hover" to="/">Blogs</router-link>
                        <router-link class="pl-4 color-white font-family-oswald font-size navlink-hover" to="/about">Create New</router-link>
                    </div>
                </div>
                <div class="col-lg-7 col-md-7 col-5">
                </div>
                <div class="col-1">
                    <div class="row w-100 row-height">
                    </div>
                    <div class="row">
                        <router-link class="pl-4 color-white font-family-oswald font-size navlink-hover border-radius" to="/about">Login</router-link>
                    </div>
                </div>
            </div>
        </div>
        <router-view />
        <Footer></Footer>
    </div>
</template>

<script>
    import Footer from './components/Footer.vue'
    export default {
        name: 'App',
        components: {
            Footer
        }
    }
</script>

<style>
    @import url('https://fonts.googleapis.com/css2?family=Oswald:wght@300&display=swap');
    #app {
        font-family: Avenir, Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
        overflow: hidden !important;
    }

    #nav {
        height: 126px;
        padding: 0px;
        background: rgba(0, 0, 0, 0.81);
    }

    .shapes-sizing {
        height: 126px;
        width: 228px;
    }

    .color-white {
        color: white;
    }

    .font-family-oswald {
        font-family: 'Oswald', sans-serif;
    }

    .font-size {
        font-size: 1.1rem;
    }

    .navlink-hover:hover{
        text-decoration: none !important;
        color: white !important;
    }

    .border-radius {
        border-radius: 60px;
        padding: 30px;
        background: rgba(255, 255, 255, 0.28);
    }

    .row-height{
        height: 16%;
    }

    .stacked-parent {
        display: grid;
        align-items: center;
    }

    .stacked-child{
        grid-area: 1 / 2;
    }

    .bloggy-link-dimensions {
        width: 120px;
        margin: auto;
    }
</style>
