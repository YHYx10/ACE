<template>
    <div class="modal-card" :id="pictures.id+'-card'" ref="modalCard" @click="modalClick">
        <div class="background">
            <svg class="background-1" xmlns="http://www.w3.org/2000/svg"  viewBox="0 0 549 386"
                fill="none">
                <path d="M453.703 -260L-192.297 386H101.604L453.703 33.9009L805.802 386H1099.7L453.703 -260Z"
                    fill="url(#paint0_linear_1_1116)" fill-opacity="0.02" />
                <!-- <defs>
                    <linearGradient id="paint0_linear_1_1116" x1="453.703" y1="-260" x2="453.703" y2="386"
                        gradientUnits="userSpaceOnUse">
                        <stop stop-color="white" />
                        <stop offset="1" stop-color="white" stop-opacity="0" />
                    </linearGradient>
                </defs> -->
            </svg>
            <svg class="background-2" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 549 640"
                fill="none">
                <path d="M453.703 0L-192.297 640H101.604L453.703 291.171L805.802 640H1099.7L453.703 0Z"
                    fill="url(#paint0_linear_1_1113)" fill-opacity="0.02" />
                <!-- <defs>
                    <linearGradient id="paint0_linear_1_1113" x1="453.703" y1="0" x2="453.703" y2="640"
                        gradientUnits="userSpaceOnUse">
                        <stop stop-color="white" />
                        <stop offset="1" stop-color="white" stop-opacity="0" />
                    </linearGradient>
                </defs> -->
            </svg>
            <svg class="background-3" xmlns="http://www.w3.org/2000/svg"  viewBox="0 0 549 426"
                fill="none">
                <path d="M453.703 0L-192.297 646H101.604L453.703 293.901L805.802 646H1099.7L453.703 0Z"
                    fill="url(#paint0_linear_1_1112)" fill-opacity="0.02" />
                <!-- <defs>
                    <linearGradient id="paint0_linear_1_1112" x1="453.703" y1="0" x2="453.703" y2="646"
                        gradientUnits="userSpaceOnUse">
                        <stop stop-color="white" />
                        <stop offset="1" stop-color="white" stop-opacity="0" />
                    </linearGradient>
                </defs> -->
            </svg>
            <div class="background-4"></div>
            <div class="background-5modal"></div>
<!--            <div class="background-6modal" :style="{ background: 'url(' + pictures.shirt + ')' }"></div>-->
<!--            <div class=" background-7modal" :style="{ background: 'url(' + pictures.pants + ')' }"></div>-->
<!--            <div class="background-8modal" :style="{ background: 'url(' + pictures.sneacker + ')' }"></div>-->
        </div>
        <div class="top-card-part">
            <div class="card-stick"></div>
            <h3 class="category-card">Category</h3>
            <h1 class="premium-category">Exclusive package#{{pictures.id}}</h1>
            <h3 class="subtitle-card">In this section you have the opportunity to acquire exclusive things</h3>
        </div>
        <div class="centre-card">
          <img class="center-shirt" :src="pictures.shirt">
          <img class="center-pants" :src="pictures.pants">
          <img class="center-sneacker" :src="pictures.sneacker">
            <div class="pluses" v-for="item in pluses" :key="item.id">
                <h3 class="pluses-img">+</h3>
                <p class="pluses-text">{{item.text}}</p>
            </div>
        </div>
        <div class="bottom-card-modal">
            <button class="exclusive-button-buy" @click="buyExclusive()">
                <span class="exclusive-button-text">Buy</span>

            </button>
            <button class="exclusive-button-try" @click="checkExclusive()">
                <span class="exclusive-button-text">Try</span>
            </button>
        </div>
    </div>
</template>

<script>
import {mapMutations} from 'vuex'
export default {
    name: 'ExclusiveCardModal',
    data() {
        return {
            pluses: [
                { id: 1, text: 'Soon...' },
                { id: 2, text: 'Soon...' },
                { id: 3, text: 'Soon...' },
                { id: 4, text: 'Soon...' },
            ]
        }
    },
    props: {
        pictures: Object
    },
    computed: {
        cssProps() {
            return {
                '--shirt': this.pictures.shirt
            }
        }
    },
    methods: {
        ...mapMutations('optionsMenu', ['setDialog']),
        modalClick() {
            this.$emit('modalClickCard', `${this.pictures.id}-card`)
            /*console.log(this.$refs.modalCard)
            this.$refs.modalCard.classList.toggle('active')*/

        },
        buyExclusive(){
            this.setDialog({
                input: undefined,
                callback: (val) => {
                    window.mp.__triggerServer("donate::buy", "exclusive", this.pictures.id);
                    if (val) return;
                },
                subtittle: 'You really want to buy EXCLUSIVE PACK #'+this.pictures.id+' ?',
            });
        },
        checkExclusive(){
			window.mp.__triggerServer("donate::check", "exclusive", this.pictures.id);
        }

    },
}
</script>

<style lang="scss" scoped>
.modal-card {
    width: 27.45rem;
    height: 38.493rem;
    border-radius: 1.35rem;
    border: 0.0495rem solid #171819;
    padding-top: 0.9495rem;
    padding-left: 1.593rem;
    box-sizing: border-box;
    overflow: hidden;
    position: relative;
    /*transition: transform 0.4s ease;

    &:hover {
        transform: scale(1.04);
        border: 1.112px solid #5CFF80;

    }*/
    .centre-card {
        margin-top: 2rem;
        height: 11.6rem;
    }
    &:hover {
        .background-4 {
            opacity: 0.2;
            transform: scale(2.6);
        }
        .center-shirt {
            transform: rotate(-5deg) translate(-4%) scale(1.02);
        }
        .center-pants {
            transform: rotate(5deg);
        }
        .center-sneacker {
            transform: translate(4%);
        }
    }
    
}

.active {
    /*border: 1.112px solid #5CFF80;
    ;*/
    .background .background-4 {
        opacity: 0.3;
        transform: scale(2.6);
    }
    .center-shirt {
            transform: rotate(-5deg) translate(-4%) scale(1.02);
    }
    .center-pants {
        transform: rotate(5deg);
    }
    .center-sneacker {
        transform: translate(4%);
    }
}

.background {
    position: absolute;

    &-1 {
        position: absolute;
      width: 27.45rem;
      height: 19.296rem;
        top: -0.9495rem;
    }

    &-2 {
        position: absolute;
      width: 27.45rem;
      height: 31.995rem;
        top: 2.493rem;
    }

    &-3 {
        position: absolute;
      width: 27.45rem;
      height: 21.294rem;
    }

    &-4 {
        position: absolute;
        width: 27.45rem;
        height: 28.296rem;
        background: radial-gradient(circle, rgba(255,255,255,1) 0%, rgba(252,70,107,0) 70%);
        opacity: 0.15;
        // filter: blur(4.4496rem);
        top: 24.948rem;
        transition: 0.3s;
        // left: 3.699rem;
    }

    &-5modal {
        width: 22.095rem;
        height: 10.593rem;
        left: -1.593rem;
        top: 20.043rem;
        position: absolute;
        background: url("/img/Shop/audi2.png");
        background-repeat: no-repeat;
      background-size: cover;
        z-index: 3;
    }

    &-6modal {
        position: absolute;
        z-index: 2;
        width: 18.297rem;
        height: 18.1499rem;
        left: 8.408rem;
        top: 1.908rem;
        background-repeat: no-repeat;
    }

    &-7modal {
        position: absolute;
        width: 26.199rem;
        height: 18.495rem;
        left: 6.05rem;
        top: 13.161rem;
        background-repeat: no-repeat;
    }

    &-8modal {
        position: absolute;
        width: 15.192rem;
        height: 8.1999rem;
        left: 9.495rem;
        z-index: 4;
        top: 26.9496rem;
        background-repeat: no-repeat;
    }
}

.card-stick {
    width: 1.296rem;
    height: 0.0999rem;
    background: #FFFFFF;
    border: 0.0495rem solid rgba(255, 255, 255, 0.09);
    box-shadow: 0px 0px 0.693rem rgba(255, 255, 255, 0.55);
    transform: rotate(-90deg);
    position: absolute;
    top: 1.998rem;
    left: 0px;
}

.category-card {
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    font-size: 0.594rem;
    line-height: 0.6993rem;
    text-transform: uppercase;

    color: rgba(255, 255, 255, 0.55);


}

.premium-category {
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    font-size: 2.3994rem;
    line-height: 2.898rem;
    /* identical to box height */

    text-transform: uppercase;

    color: #FFFFFF;
}

.subtitle-card {
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    font-size: 0.594rem;
    line-height: 0.693rem;
    text-transform: uppercase;

    color: rgba(255, 255, 255, 0.55);
    max-width: 10.197rem;
}
.center {
    
  &-shirt {
    position: absolute;
    width: 19.197rem;
    height: 19.8rem;
    z-index: 2;
    left: 8.408rem;
    top: 1.908rem;
    transition: 0.3s;
  }
  &-pants {
    position: absolute;
    width: 18.999rem;
    height: 19.2492rem;
    left: 6.0498rem;
    top: 14.961rem;
    transition: 0.3s;
  }
  &-sneacker {
    position: absolute;
    left: 9.85rem;
    width: 14.292rem;
    height: 7.794rem;
    z-index: 4;
    top: 27.5499rem;
    transition: 0.3s;
  }
}
.pluses {
    height: 2.0493rem;
    display: flex;
    margin-top: 0.4rem;
    &-img {
        font-family: 'Akrobat';
        font-style: normal;
        font-weight: 900;
        font-size: 2.394rem;
        line-height: 2.898rem;
        /* identical to box height */

        text-transform: uppercase;

        color: #5CFF80;

        text-shadow: 0px 0px 0.693rem rgba(92, 255, 128, 0.25);
        margin-right: 0.5994rem;
    }

    &-text {
        font-family: 'Akrobat';
        font-style: normal;
        font-weight: 700;
        font-size: 0.792rem;
        line-height: 0.9499rem;
        text-transform: uppercase;

        color: rgba(255, 255, 255, 0.85);
        margin-top: 1.1493rem;
    }
}

.bottom-card-modal {
    display: flex;
  margin-left: 0.9999rem;
    margin-top: 14.598rem;
}

.exclusive-button-buy {
    z-index: 999;
    width: 10.9655rem;
    height: 2.3499rem;
    left: 2.592rem;
    top: 36.1499rem;
    margin-right: 0.3845rem;
    background: linear-gradient(180deg, rgba(92, 255, 128, 0.25) 0%, rgba(17, 90, 33, 0.25) 100%);
    border: 1px solid #5CFF80;
    box-shadow: inset 0px 0px 15px rgb(92 255 128 / 86%);
    transition: 0.5s ease;
    &:hover {
        background: linear-gradient(180deg, rgba(92, 255, 128, 0.3) 0%, rgba(17, 90, 33, 0.3) 300%);
        box-shadow: inset 0px 0px 7.5rem #5CFF80;
        filter: drop-shadow(0px 0px 15px rgba(92, 255, 128, 0.3));
    }
}

.exclusive-button-try {
    z-index: 999;
    width: 10.9655rem;
    height: 2.3499rem;
    left: 13.95rem;
    top: 36.1494rem;
    //transition: opacity 0.4s ease;
    background: linear-gradient(180deg, rgba(255, 229, 92, 0.25) 0%, rgba(90, 70, 17, 0.25) 100%);
    border: 0.0495rem solid #FFE55C;
    border: 1px solid #FFE55C;
    box-shadow: inset 0px 0px 15px rgb(255 229 92 / 86%);
    transition: 0.5s ease;
    &:hover {
        background: linear-gradient(180deg, rgba(255, 229, 92, 0.3) 0%, rgba(90, 70, 17, 0.3) 300%);
        box-shadow: inset 0px 0px 7.5rem #FFE55C;
        filter: drop-shadow(0px 0px 15px rgba(92, 255, 128, 0.3));
    }

}

.exclusive-button-text {
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 600;
    font-size: 0.693rem;
    line-height: 0.8496rem;
    /* identical to box height */

    text-align: center;
    text-transform: uppercase;

    color: #FFFFFF;

}
</style>