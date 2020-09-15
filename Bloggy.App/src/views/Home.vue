<template>
    <div class="home">
        <div class="row w-100 home-container m-0" style="height: 80vh;">
            <div class="row w-100 opacity-white height-10">

            </div>
        </div>
    </div>
</template>

<script>

    export default {
        name: 'Home',
        components: {
        }
    }
</script>

<style>
    .home-container {
        background-image: url(../assets/background-final.png);
        background-size: cover;
    }

    .opacity-white {
        opacity: 0.35;
        background: white;
    }

    .height-10 {
        height: 10px;
    }

    .home {
        overflow: hidden;
    }
</style>
