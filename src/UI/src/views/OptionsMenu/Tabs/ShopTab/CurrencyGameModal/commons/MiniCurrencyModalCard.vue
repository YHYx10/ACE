<template>
  <div class="item-card-object" :id="currencyCard.id+'-item'" @click="$emit('currencyModalClick', `${currencyCard.id}-item`)">
    <div class="background">
      <svg class="background-1" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 340 386"
           fill="none">
        <path d="M453.702 -260L-192.298 386H101.603L453.702 33.9009L805.801 386H1099.7L453.702 -260Z"
              fill="url(#paint0_linear_1_232)" fill-opacity="0.02" />
        <defs>
          <linearGradient id="paint0_linear_1_232" x1="453.702" y1="-260" x2="453.702" y2="386"
                          gradientUnits="userSpaceOnUse">
            <stop stop-color="white" />
            <stop offset="1" stop-color="white" stop-opacity="0" />
          </linearGradient>
        </defs>
      </svg>
      <svg class="background-2" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 340 349"
           fill="none">
        <path d="M453.702 0L-192.298 640H101.603L453.702 291.171L805.801 640H1099.7L453.702 0Z"
              fill="url(#paint0_linear_1_231)" fill-opacity="0.02" />
        <defs>
          <linearGradient id="paint0_linear_1_231" x1="453.702" y1="0" x2="453.702" y2="640"
                          gradientUnits="userSpaceOnUse">
            <stop stop-color="white" />
            <stop offset="1" stop-color="white" stop-opacity="0" />
          </linearGradient>
        </defs>
      </svg>
      <div class="background-3"></div>
      <!--            <div class="background-4d"></div>-->
    </div>
    <div class="top-card">
      <div class="left-card-part">
        <div class="card-stick"></div>
        <h3 class="category-card">Category</h3>
        <h1 class="premium-header">A visiting type</h1>
        <h3 class="subtitle-card">In this section you have the opportunity
        Buy clothes of different classes</h3>
      </div>
      <div class="right-card-part">
        <div class="right-card-text">
          <h3 class="subtitle-donate">Coin</h3>
          <h2 class="count-premium">750,000</h2>
        </div>

        <svg class="right-coin" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 72 72"
             fill="none">
          <g filter="url(#filter0_d_1_206)">
            <path
                d="M56.0007 36.1493C56.0007 47.2774 46.9795 56.2985 35.8514 56.2985C24.7233 56.2985 15.7021 47.2774 15.7021 36.1493C15.7021 25.0211 24.7233 16 35.8514 16C46.9795 16 56.0007 25.0211 56.0007 36.1493ZM17.1466 36.1493C17.1466 46.4797 25.521 54.8541 35.8514 54.8541C46.1818 54.8541 54.5562 46.4797 54.5562 36.1493C54.5562 25.8189 46.1818 17.4444 35.8514 17.4444C25.521 17.4444 17.1466 25.8189 17.1466 36.1493Z"
                fill="#5CFF80" />
          </g>
          <circle cx="35.8514" cy="36.1492" r="14.5075" fill="white" fill-opacity="0.06" />
          <g filter="url(#filter1_d_1_206)">
            <path fill-rule="evenodd" clip-rule="evenodd"
                  d="M34.9229 26.5017C30.8352 26.9251 27.4383 29.86 26.4502 33.8222C26.231 34.7012 26.1798 35.1415 26.1798 36.1497C26.1798 36.8196 26.1969 37.1511 26.2489 37.4931C26.7963 41.0901 29.1765 44.0013 32.6134 45.2777C33.0639 45.4449 33.9862 45.6701 34.5443 45.7489C35.2253 45.8451 36.4775 45.845 37.1566 45.7486C41.0798 45.1916 44.295 42.3033 45.2509 38.4771C45.458 37.6485 45.5231 37.0914 45.5231 36.1497C45.5231 35.2079 45.458 34.6508 45.2509 33.8222C44.3838 30.3517 41.6278 27.603 38.1599 26.75C37.8579 26.6757 37.432 26.5881 37.2134 26.5552C36.7361 26.4836 35.3993 26.4523 34.9229 26.5017ZM36.5752 28.7719C39.7931 29.0985 42.4673 31.513 43.1167 34.6779C43.7264 37.6494 42.4434 40.6811 39.8636 42.365C36.7389 44.4045 32.6022 43.7814 30.1861 40.9072C29.3031 39.8568 28.7751 38.7141 28.5211 37.3039C28.429 36.7925 28.4263 35.5323 28.5162 35.0143C28.7072 33.9134 29.0948 32.9314 29.6844 32.0549C31.2248 29.765 33.8827 28.4987 36.5752 28.7719ZM35.1567 29.9075C32.7378 30.1885 30.6573 31.8793 29.8903 34.1874C29.2807 36.0221 29.5475 37.9979 30.6211 39.5992C32.119 41.8335 34.7944 42.8843 37.3507 42.2424C39.9078 41.6003 41.7942 39.4408 42.0957 36.8104C42.4111 34.0584 40.7527 31.3393 38.1268 30.3028C37.2147 29.9427 36.114 29.7962 35.1567 29.9075ZM38.388 33.3875C38.4842 33.4366 38.5646 33.5169 38.6137 33.6131C38.6877 33.7581 38.6899 33.8318 38.6899 36.1497C38.6899 38.4675 38.6877 38.5412 38.6137 38.6862C38.5646 38.7824 38.4842 38.8628 38.388 38.9118C38.2429 38.9858 38.1692 38.988 35.8504 38.988C33.5317 38.988 33.4579 38.9858 33.3129 38.9118C33.2167 38.8628 33.1362 38.7824 33.0872 38.6862C33.0132 38.5412 33.011 38.4675 33.011 36.1497C33.011 33.8328 33.0132 33.7581 33.0871 33.6133C33.1336 33.5221 33.2183 33.4351 33.3048 33.3894C33.4413 33.3174 33.5317 33.3146 35.8425 33.313C38.171 33.3114 38.2428 33.3135 38.388 33.3875ZM34.1468 36.1497V37.8527H35.8504H37.5541V36.1497V34.4467H35.8504H34.1468V36.1497Z"
                  fill="#5CFF80" />
          </g>
          <defs>
            <filter id="filter0_d_1_206" x="4.41857" y="4.71642" width="62.8657" height="62.8657"
                    filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
              <feFlood flood-opacity="0" result="BackgroundImageFix" />
              <feColorMatrix in="SourceAlpha" type="matrix"
                             values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0" result="hardAlpha" />
              <feOffset />
              <feGaussianBlur stdDeviation="5.64179" />
              <feComposite in2="hardAlpha" operator="out" />
              <feColorMatrix type="matrix"
                             values="0 0 0 0 0.360784 0 0 0 0 1 0 0 0 0 0.501961 0 0 0 0.25 0" />
              <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow_1_206" />
              <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow_1_206" result="shape" />
            </filter>
            <filter id="filter1_d_1_206" x="0.388399" y="0.686571" width="70.9258" height="70.9254"
                    filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
              <feFlood flood-opacity="0" result="BackgroundImageFix" />
              <feColorMatrix in="SourceAlpha" type="matrix"
                             values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0" result="hardAlpha" />
              <feOffset />
              <feGaussianBlur stdDeviation="12.8955" />
              <feColorMatrix type="matrix"
                             values="0 0 0 0 0.360784 0 0 0 0 1 0 0 0 0 0.501961 0 0 0 0.25 0" />
              <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow_1_206" />
              <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow_1_206" result="shape" />
            </filter>
          </defs>
        </svg>
      </div>
    </div>
    <img class="centre-img" :src="currencyCard.path" :style="{width: currencyCard.width, height: currencyCard.height}">
    <div class="bottom-card-modal">
      <button class="exclusive-button-buy">
        <span class="exclusive-button-text">Buy</span>

      </button>
    </div>
  </div>
</template>

<script>
export default {
  name: 'MiniCurrencyCard',
  props: {
    currencyCard: Object,
  },
  methods: {
    modalClick() {
      this.$emit('modalClick', `${this.currencyCard.id}-item`)
    }

  }
}
</script>

<style lang="scss" scoped>
.item-card-object {
  position: relative;
  width: 18.88rem;
  height: 21.88rem;
  border-radius: 1.5rem;
  border: 1px solid #1E1E1E;
  padding-top: 1.055rem;
  padding-left: 1.944rem;
  overflow: hidden;
  /*transition: transform 0.4s ease;

  &:hover {
      transform: scale(1.04);
      border: 1px solid #5CFF80 !important;


  }*/
}

/*.active {
    border: 1px solid #5CFF80 !important;
    ;
}*/

.background {
  position: absolute;

  &-1 {
    position: absolute;
    width: 18.88rem;
    height: 21.44rem;
    top: -1rem;
    left: -1.88rem;
  }

  &-2 {
    position: absolute;
    width: 18.88rem;
    height: 19.388rem;
    top: 2.055rem;
    left: -2.11rem;

  }

  &-3 {
    position: absolute;
    width: 10.55rem;
    height: 10.55rem;
    background: #FFFFFF;
    opacity: 0.25;
    filter: blur(4.944rem);
    top: 15.88rem;
    left: 2.722rem;
  }

  &-4d {
    position: absolute;
    width: 9.77rem;
    height: 10.33rem;
    background: url("/img/Shop/shirt-object-modal.png");
    top: 7.44rem;
    left: 2.55rem;
  }
}

.top-card {
  display: flex;
}

.left-card-part {
  margin-right: 2.11rem;
}

.card-stick {
  width: 1.44rem;
  height: 0.11rem;
  position: absolute;
  top: 2.277rem;
  left: 0.44rem;
  background-color: #FFFFFF;
  border: 0.055rem solid rgba(255, 255, 255, 0.09);
  box-shadow: 0px 0px 0.77rem rgba(255, 255, 255, 0.55);
  transform: rotate(-90deg);

}

.category-card {
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 700;
  font-size: 0.66rem;
  line-height: 0.77rem;
  text-transform: uppercase;

  color: rgba(255, 255, 255, 0.55);


}

.premium-header {
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 700;
  font-size: 1.33rem;
  line-height: 103%;
  max-width: 5.722rem;
  /* or 25px */

  text-transform: uppercase;

  color: #FFFFFF;

}

.subtitle-card {

  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 700;
  font-size: 0.666rem;
  line-height: 0.777rem;
  text-transform: uppercase;

  color: rgba(255, 255, 255, 0.55);
  max-width: 11.33rem;
;
}

.right-card-part {
  padding-top: 1.33rem;
  display: flex;
  flex-direction: column;
}

.right-card-text {
  position: absolute;
  left: 12.388rem;
  top: 1.11rem;
}

.subtitle-donate {
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 700;
  font-size: 0.6268666rem;
  line-height: 0.77rem;
  /* identical to box height */

  text-align: right;
  text-transform: uppercase;

  color: rgba(255, 255, 255, 0.25);
}

.count-premium {
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 700;
  font-size: 1.11rem;
  line-height: 1.33rem;
  /* identical to box height */

  text-transform: uppercase;

  color: #5CFF80;
}

.right-coin {
  position: absolute;
  width: 4rem;
  height: 4rem;
  top: 0.22rem;
  left: 15.277rem;
}
.centre-img {
  position: absolute;
  margin-left: auto;
  margin-right: auto;
  left: 0;
  right: 0;
  z-index: 99;
  //margin-top: 1rem;
  width: 9.77rem;
  height: 10.33rem;
}
.bottom-card-modal {
  display: flex;
  margin-top: 11.666rem;
  margin-left: -0.167rem;
}

.exclusive-button-buy {
  z-index: 9999;
  width: 15.166rem;
  height: 2.611rem;
  cursor: pointer;
  background: linear-gradient(180deg, rgba(92, 255, 128, 0.25) 0%, rgba(17, 90, 33, 0.25) 100%);
  border: 0.055rem solid #5CFF80;
  transition: opacity .1s ease-out;
  &:hover {
    //background: linear-gradient(180deg, rgba(255, 229, 92, 0.25) 0%, rgba(90, 70, 17, 0.25) 100%);
    opacity: 0.6;
  }
}

.exclusive-button-text {
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 600;
  font-size: 0.777rem;
  line-height: 0.944rem;
  /* identical to box height */

  text-align: center;
  text-transform: uppercase;

  color: #FFFFFF;

}
</style>