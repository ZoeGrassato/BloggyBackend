<template>
    <div class="footer">
        <div class="row w-100 h-100">
            <div class="col-9 col-md-10 col-lg-11">
            </div>
            <div class="col-lg-1 col-md-2 col-3">
                <div class="row positioning">
                        <span class="font-family-oswald color-white">
                            Bloggy Data-@2020
                        </span>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'Footer',
        components: {
        }
    }
</script>

<style>

    .positioning {
        padding-top: 7px;
    }

    .footer {
        height: 61px;
        background: #404040;
    }

    .font-family-oswald {
        font-family: 'Oswald', sans-serif;
    }

    .color-white {
        color: white;
    }
</style>