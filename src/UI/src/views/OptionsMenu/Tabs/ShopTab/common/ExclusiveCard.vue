<template>
    <div class="exclusive-part">
        <div class="background">
            
            <div class="background-89"></div>
<!--            <div class="background-5"></div>-->
<!--            <div class="background-6"></div>-->
<!--            <div class="background-7"></div>-->
<!--            <div class="background-8"></div>-->

        </div>
        <div class="top-card">
            <div class="left-card-part">
                <div class="card-stick"></div>
                <h3 class="category-card">Category</h3>
                <h1 class="premium-category"> Exclusive package</h1>
                <h3 class="subtitle-card">In this section you can buy exclusive packages (the number is limited)</h3>
            </div>
            <div class="right-cart-part">
                <div class="limited">
                    <span class="limited-text">{{exclusive.count}}/{{exclusive.maxcount}} left</span>
                </div>
            </div>
        </div>
        <div class="centre-card">
          <img class="center-car" src="/img/Shop/audi1.png">
          <img class="center-foot" src="/img/Shop/feet_0321.png">
          <img class="center-shirt-1" src="/img/Shop/shirt-1.png">
          <img class="center-shirt-2" src="/img/Shop/shirt-2.png">
            <div class="pluses" v-for="item in pluses" :key="item.id">
                <h3 class="pluses-img">+</h3>
                <p class="pluses-text">{{item.text}}</p>

            </div>
        </div>
        <div class="bottom-card">
            <img class="bottom-card-coin" src="/img/Shop/green-coin.png" alt="">
            <svg  v-if="false" class="bottom-card-coin" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 88 88"
                fill="none">
                <g filter="url(#filter0_d_1_161)">
                    <path
                        d="M69 44C69 57.8071 57.8071 69 44 69C30.1929 69 19 57.8071 19 44C19 30.1929 30.1929 19 44 19C57.8071 19 69 30.1929 69 44ZM20.7921 44C20.7921 56.8173 31.1827 67.2079 44 67.2079C56.8173 67.2079 67.2079 56.8173 67.2079 44C67.2079 31.1827 56.8173 20.7921 44 20.7921C31.1827 20.7921 20.7921 31.1827 20.7921 44Z"
                        fill="#5CFF80" />
                </g>
                <circle cx="44" cy="44" r="18" fill="white" fill-opacity="0.06" />
                <g filter="url(#filter1_d_1_161)">
                    <path fill-rule="evenodd" clip-rule="evenodd"
                        d="M42.8478 32.0297C37.7761 32.5551 33.5614 36.1966 32.3354 41.1127C32.0635 42.2033 32 42.7495 32 44.0004C32 44.8316 32.0211 45.2429 32.0857 45.6673C32.7649 50.1302 35.718 53.7422 39.9824 55.3259C40.5413 55.5334 41.6857 55.8128 42.3781 55.9106C43.2231 56.03 44.7767 56.0298 45.6193 55.9102C50.4869 55.2192 54.4762 51.6355 55.6623 46.8881C55.9192 45.8601 56 45.1688 56 44.0004C56 42.832 55.9192 42.1407 55.6623 41.1127C54.5864 36.8067 51.1669 33.3963 46.8641 32.3379C46.4895 32.2457 45.961 32.137 45.6898 32.0962C45.0976 32.0073 43.439 31.9685 42.8478 32.0297ZM44.898 34.8466C48.8905 35.2518 52.2085 38.2475 53.0142 42.1743C53.7707 45.8611 52.1788 49.6228 48.978 51.712C45.1011 54.2426 39.9685 53.4694 36.9707 49.9033C35.8751 48.6 35.22 47.1822 34.9049 45.4325C34.7906 44.798 34.7873 43.2345 34.8988 42.5918C35.1358 41.2258 35.6167 40.0074 36.3482 38.9199C38.2594 36.0787 41.5572 34.5076 44.898 34.8466ZM43.1379 36.2555C40.1367 36.6041 37.5553 38.702 36.6038 41.5657C35.8474 43.8422 36.1784 46.2936 37.5104 48.2804C39.369 51.0525 42.6885 52.3563 45.8601 51.5599C49.0329 50.7633 51.3734 48.0838 51.7474 44.8202C52.1387 41.4057 50.0811 38.032 46.8231 36.746C45.6914 36.2992 44.3257 36.1175 43.1379 36.2555ZM47.1472 40.5733C47.2665 40.6342 47.3663 40.7339 47.4272 40.8532C47.519 41.0331 47.5218 41.1246 47.5218 44.0004C47.5218 46.8763 47.519 46.9677 47.4272 47.1476C47.3663 47.2669 47.2665 47.3667 47.1472 47.4275C46.9672 47.5193 46.8757 47.522 43.9987 47.522C41.1217 47.522 41.0302 47.5193 40.8502 47.4275C40.7309 47.3667 40.6311 47.2669 40.5702 47.1476C40.4784 46.9677 40.4757 46.8763 40.4757 44.0004C40.4757 41.1258 40.4784 41.033 40.5701 40.8534C40.6279 40.7403 40.7329 40.6323 40.8402 40.5756C41.0096 40.4862 41.1217 40.4829 43.9888 40.4809C46.8779 40.4788 46.967 40.4815 47.1472 40.5733ZM41.8849 44.0004V46.1134H43.9987H46.1125V44.0004V41.8874H43.9987H41.8849V44.0004Z"
                        fill="#5CFF80" />
                </g>
                <defs>
                    <filter id="filter0_d_1_161" x="5" y="5" width="78" height="78" filterUnits="userSpaceOnUse"
                        color-interpolation-filters="sRGB">
                        <feFlood flood-opacity="0" result="BackgroundImageFix" />
                        <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0"
                            result="hardAlpha" />
                        <feOffset />
                        <!-- <feGaussianBlur stdDeviation="7" /> -->
                        <feComposite in2="hardAlpha" operator="out" />
                        <feColorMatrix type="matrix"
                            values="0 0 0 0 0.360784 0 0 0 0 1 0 0 0 0 0.501961 0 0 0 0.25 0" />
                        <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow_1_161" />
                        <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow_1_161" result="shape" />
                    </filter>
                    <filter id="filter1_d_1_161" x="0" y="0" width="88" height="88" filterUnits="userSpaceOnUse"
                        color-interpolation-filters="sRGB">
                        <feFlood flood-opacity="0" result="BackgroundImageFix" />
                        <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0"
                            result="hardAlpha" />
                        <feOffset />
                        <!-- <feGaussianBlur stdDeviation="16" /> -->
                        <feColorMatrix type="matrix"
                            values="0 0 0 0 0.360784 0 0 0 0 1 0 0 0 0 0.501961 0 0 0 0.25 0" />
                        <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow_1_161" />
                        <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow_1_161" result="shape" />
                    </filter>
                </defs>
            </svg>
            <div class="bottom-card-text-part">
                <h3 class="bottom-card-text">Price</h3>
                <h2 class="bottom-card-amount">{{exclusive.price}}</h2>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'ExclusiveCard',
    props: {
        exclusive: Object
    },
    data() {
        return {
            pluses: [
                { id: 1, text: 'Soon...' },
                { id: 2, text: 'Soon...' },
                { id: 3, text: 'Soon...' },
                { id: 4, text: 'Soon... ' },
            ]
        }
    }
}
</script>

<style lang="scss">
* {
  box-sizing: border-box;
}
.exclusive-part {
    width: 38.7rem;
    height: 20.2rem;
    border: 0.0563rem solid #171819;
    border-radius: 1.5188rem;
    position: relative;
    padding-top: 1.0688rem;
    padding-left: 1.9688rem;
    overflow: hidden;
    &:hover {
        .background-89 {
            transform: scale(1.8);
            opacity: .3;
        }
        .center-shirt-1 {
            transform: translate(2%);
        }
        .center-shirt-2 {
            transform: translate(-2%) rotate(5deg);
        }
        .center-foot {
            transform: scale(1.05) rotate(-2deg);
        }
    }
    /*transition: transform 0.4s ease;

    &:hover {
        transform: scale(1.04);

    }*/
}

.background {
    position: absolute;

    &-1 {
        position: absolute;
      width: 38.745rem;
      height: 19.296rem;
        left: 0;
        top: 0.9rem;
    }

    &-2 {
        position: absolute;
      width: 38.745rem;
      height: 17.946rem;
        left: 0;
        top: 3.825rem;
    }

    &-3 {
        position: absolute;
      width: 38.745rem;
      height: 2.097rem;
        left: 0;
        top: 22.275rem;

    }

    &-89 {
        position: absolute;
        width: 27rem;
        height: 14.2875rem;
        left: 0;
        top: 4.275rem;
        background: radial-gradient(circle, rgba(255,255,255,1) 0%, rgba(255,255,255,0) 52%);
        opacity: 0.25;
        transition: .2s;
        width: 38.7rem;
        z-index: 0;
        height: 29.2rem;
        // filter: blur(5.0062rem);
    }

}

.top-card {
    display: flex;
}

.left-card-part {
    margin-right: 1.6875rem;
}

.card-stick {
    width: 1.4625rem;
    height: 0.1125rem;
    background: #FFFFFF;
    border: 0.0563rem solid rgba(255, 255, 255, 0.09);
    box-shadow: 0 0 0.0788rem rgba(255, 255, 255, 0.55);
    transform: rotate(-90deg);
    position: absolute;
    top: 2.25rem;
    left: 0;
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

.premium-category {
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

.limited {
    width: 8.6062rem;
    height: 1.9688rem;
    margin-top: 1.5188rem;
    background: rgba(92, 255, 128, 0.25);
    border: 0.1125rem solid #5CFF80;
    border-radius: 1.0125rem;
    display: flex;
    align-items: center;

    &-text {
        font-family: 'Akrobat';
        font-style: normal;
        font-weight: 700;
        font-size: 0.9rem;
        line-height: 1.0688rem;
        text-transform: uppercase;

        color: #FFFFFF;
        margin: 0 auto;
    }
}
.center {
  &-car {
    z-index: 10;
    width: 25.047rem;
    height: 11.142rem;
    position: absolute;
    top: 9.2813rem;
    left: 13.5rem;
  }
  &-foot {
    z-index: 11;
    width: 11.25rem;
    height: 5.6813rem;
    position: absolute;
    top: 14.625rem;
    left: 10.8562rem;
    transition: 0.4s;
  }
  &-shirt-1 {
    position: absolute;
    width: 14.004rem;
    height: 11.25rem;
    top: 4.725rem;
    left: 13.2188rem;
    transition: 0.4s;
    // filter: blur(0.0563rem);
    //transform: rotate(-15deg);
  }
  &-shirt-2 {
    position: absolute;
    width: 17.6625rem;
    height: 14.7937rem;
    top: 1.4063rem;
    left: 20.5313rem;
    transition: 0.4s;
  }
}
.pluses {
    height: 2.0362rem;
    display: flex;

    &-img {
        font-family: 'Akrobat';
        font-style: normal;
        font-weight: 900;
        font-size: 2.7rem;
        line-height: 3.2625rem;
        /* identical to box height */

        text-transform: uppercase;

        color: #5CFF80;

        text-shadow: 0 0 0.7875rem rgba(92, 255, 128, 0.25);
        margin-right: 0.675rem;
    }

    &-text {
        font-family: 'Akrobat';
        font-style: normal;
        font-weight: 700;
        font-size: 0.81rem;
        line-height: 1.0688rem;
        text-transform: uppercase;

        color: rgba(255, 255, 255, 0.85);
        margin-top: 1.2937rem;
    }
}

.bottom-card {
    margin-top: 0.36rem;
    display: flex;

    &-coin {
        margin-left: -0.9rem;
      width: 4.392rem;
      height: 4.392rem;
    }

    &-text-part {
        align-self: center;
    }

    &-text {
        font-family: 'Akrobat';
        font-style: normal;
        font-weight: 700;
        font-size: 0.7875rem;
        line-height: 0.9563rem;
        /* identical to box height */

        text-transform: uppercase;

        color: rgba(255, 255, 255, 0.25);
    }

    &-amount {
        font-family: 'Akrobat';
        font-style: normal;
        font-weight: 700;
        font-size: 1.125rem;
        line-height: 1.35rem;
        /* identical to box height */

        text-transform: uppercase;

        color: #5CFF80;
    }
}
</style>