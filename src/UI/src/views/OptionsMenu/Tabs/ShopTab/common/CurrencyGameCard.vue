<template>
    <div class="current-card">
        <div class="background">
            <svg class="background-1" xmlns="http://www.w3.org/2000/svg"  viewBox="0 0 448 640"
                fill="none">
                <path d="M453.702 0L-192.298 640H101.603L453.702 291.171L805.801 640H1099.7L453.702 0Z"
                    fill="url(#paint0_linear_1_43)" fill-opacity="0.02" />
                <!-- <defs>
                    <linearGradient id="paint0_linear_1_43" x1="453.702" y1="0" x2="453.702" y2="640"
                        gradientUnits="userSpaceOnUse">
                        <stop stop-color="white" />
                        <stop offset="1" stop-color="white" stop-opacity="0" />
                    </linearGradient>
                </defs> -->
            </svg>
            <svg class="background-2" xmlns="http://www.w3.org/2000/svg"  viewBox="0 0 448 481"
                fill="none">
                <path d="M453.702 0L-192.298 646H101.603L453.702 293.901L805.801 646H1099.7L453.702 0Z"
                    fill="url(#paint0_linear_1_42)" fill-opacity="0.02" />
                <!-- <defs>
                    <linearGradient id="paint0_linear_1_42" x1="453.702" y1="0" x2="453.702" y2="646"
                        gradientUnits="userSpaceOnUse">
                        <stop stop-color="white" />
                        <stop offset="1" stop-color="white" stop-opacity="0" />
                    </linearGradient>
                </defs> -->
            </svg>
            <svg class="background-3" xmlns="http://www.w3.org/2000/svg"  viewBox="0 0 448 481"
                fill="none">
                <path d="M453.702 0L-192.298 646H101.603L453.702 293.901L805.801 646H1099.7L453.702 0Z"
                    fill="url(#paint0_linear_1_42)" fill-opacity="0.02" />
                <!-- <defs>
                    <linearGradient id="paint0_linear_1_42" x1="453.702" y1="0" x2="453.702" y2="646"
                        gradientUnits="userSpaceOnUse">
                        <stop stop-color="white" />
                        <stop offset="1" stop-color="white" stop-opacity="0" />
                    </linearGradient>
                </defs> -->
            </svg>
            <div class="background-4"></div>
            <div class="background-5r"></div>
            <div class="background-6r"></div>
<!--            <div class="background-7r"></div>-->
        </div>
        <div class="top-card">
            <div class="card-stick"></div>
            <h3 class="category-card">Category</h3>
            <h1 class="current-category" style="margin-bottom:-0.75rem">Spiel</h1>
            <h1 class="current-category">Currency</h1>
            <h3 class="subtitle-card"> In this section you can buy a game currency</h3>
        </div>
      <img class="current-center-img" src="/img/Shop/gold.png">
        <div class="bottom-card-c">
            <button class="buy-button">
                <span class="buy-button-text">
                Read more inside
                </span>
            </button>
        </div>
    </div>
</template>

<script>
export default {
    name: "CurrencyGameCard"
}
</script>

<style lang="scss" scoped>
* {
  box-sizing: border-box;
}
.current-card {
  width: 22.4rem;
  height: 40.35rem;
    border: 0.0563rem solid #171819;
    border-radius: 1.5188rem;
    position: relative;
    overflow: hidden;
    /*transition: transform 0.4s ease;

    &:hover {
        transform: scale(1.04);

    }*/
    &:hover {
        .background-4 {
            opacity: 0.30;
        }
        .background-5r {
            // transform: scale(1) translate(0, -2%) rotate(160deg);
        }
        .background-6r {
            // transform: translate(0, 2%);
        }
    }
}

.background {
    position: absolute;

    &-1 {
        position: absolute;
      width: 22.392rem;
      height: 31.995rem;
        top: -14.3438rem;
        left: 5.4rem;
    }

    &-2 {
        position: absolute;
      width: 22.392rem;
      height: 24.0498rem;
        top: 4.6688rem;
        left: 5.4rem;
    }

    &-3 {
        position: absolute;
      width: 22.392rem;
      height: 24.0498rem;
        top: 25.65rem;
        left: 5.4rem;
    }

    &-4 {
        background: #FFBC10;
        opacity: 0.15;
        // top: 4.5rem;
        width: 22.4rem;
        position: absolute;
        // filter: blur(9rem);
        width: 35.1rem;
        height: 40.35rem;
        background: linear-gradient(0deg, rgba(255,188,16,1) 0%, rgba(252,70,107,0) 100%);
        transition: .3s;
        z-index: -2;
        
        // background: radial-gradient(circle, rgba(255,188,16,1) 0%, rgba(252,70,107,0) 60%);
    }

    &-5r {
        background: url("/img/Shop/cur-left.png") no-repeat;
        position: absolute;
        width: 29.475rem;
        height: 32.0625rem;
        left: -23.4563rem;
        top: 25.5375rem;
        filter: blur(0.8718rem);
        transform: rotate(160deg);
        // transform: matrix(0, 1, 1, 0, 0, 0);
      background-size: contain;
      transition: .3s;
    }

    &-6r {
        background: url("/img/Shop/cur-right.png");
        position: absolute;
        width: 29.475rem;
        height: 32.1183rem;
        left: 13.95rem;
        top: 5.7938rem;
        background-size: contain;
        filter: blur(0.495rem);
        transition: .3s;

    }

    &-7r {
        //background: url("/img/Shop/gold.png");
        //position: absolute;
        //width: 357.778px;
        //height: 242.222px;
        //top: 366.666px;
        //left: 62.222px;
    }
}

.top-card {
    display: block;
    padding-top: 1.0688rem;
    padding-left: 1.6875rem;
}

.card-stick {
    width: 1.4625rem;
    height: 0.1125rem;
    background: #FFFFFF;
    border: 0.0563rem solid rgba(255, 255, 255, 0.09);
    box-shadow: 0px 0px 0.7875rem rgba(255, 255, 255, 0.55);
    transform: rotate(-90deg);
    position: absolute;
    top: 2.25rem;
    left: 0px;
}

.category-card {
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    font-size: 0.675rem;
    line-height: 0.7875rem;
    text-transform: uppercase;

    color: rgba(255, 255, 255, 0.55);


}

.current-category {
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    font-size: 2.7rem;
    line-height: 3.2625rem;
    /* identical to box height */

    text-transform: uppercase;

    color: #FFFFFF;
}

.subtitle-card {
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    font-size: 0.675rem;
    line-height: 0.7875rem;
    text-transform: uppercase;

    color: rgba(255, 255, 255, 0.55);
    max-width: 11.475rem;
}
.current-center-img {
  position: absolute;
  width: 18.1125rem;
  height: 12.2625rem;
  top: 14.9625rem;
  left: 1.8rem;
}
.buy-button {
  z-index: 99;
  position: absolute;
    margin-top: 25.875rem;
    background: linear-gradient(180deg, rgba(255, 170, 92, 0.25) 0%, rgba(92, 53, 17, 0.25) 100%);
    border: 0.0563rem solid #FFAA5C;
    width: 18rem;
    height: 3.7125rem;
    margin-left: 2.0812rem;
    display: flex;
    justify-content: center;
    align-items: center;
    cursor: pointer;
    border: 1px solid #FFAA5C;
    box-shadow: inset 0px 0px 15px rgb(255 170 92 / 86%);
    transition: 0.5s ease;
    &:hover {
        // opacity: 0.6;
        background: linear-gradient(180deg, rgba(255, 170, 92, 0.7) 0%, rgba(92, 53, 17, 0.7) 300%);
        box-shadow: inset 0px 0px 7.5rem #FFAA5C;
        filter: drop-shadow(0px 0px 15px rgba(255, 170, 92, 0.7));
    }
    &-text {
        font-family: 'Akrobat';
        font-style: normal;
        font-weight: 600;
        font-size: 0.7875rem;
        line-height: 0.0563rem;
        /* identical to box height */

        text-align: center;
        text-transform: uppercase;

        color: #FFFFFF;

    }
}
</style>