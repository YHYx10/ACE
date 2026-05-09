<template>
  <div class="item-card-object" :id="animalCard.id+'-animal-card'" @click="$emit('animalModalClick', `${animalCard.id}-animal-card`)">
    <div class="background">
      <svg class="background-1" xmlns="http://www.w3.org/2000/svg"  viewBox="0 0 439 386" fill="none">
        <path d="M453.703 -260L-192.297 386H101.604L453.703 33.9009L805.802 386H1099.7L453.703 -260Z" fill="url(#paint0_linear_1_652)" fill-opacity="0.02"/>
        <!-- <defs>
          <linearGradient id="paint0_linear_1_652" x1="453.703" y1="-260" x2="453.703" y2="386" gradientUnits="userSpaceOnUse">
            <stop stop-color="white"/>
            <stop offset="1" stop-color="white" stop-opacity="0"/>
          </linearGradient>
        </defs> -->
      </svg>
      <svg class="background-2" xmlns="http://www.w3.org/2000/svg"  viewBox="0 0 439 323" fill="none">
        <path d="M453.703 0L-192.297 640H101.604L453.703 291.171L805.802 640H1099.7L453.703 0Z" fill="url(#paint0_linear_1_649)" fill-opacity="0.02"/>
        <!-- <defs>
          <linearGradient id="paint0_linear_1_649" x1="453.703" y1="0" x2="453.703" y2="640" gradientUnits="userSpaceOnUse">
            <stop stop-color="white"/>
            <stop offset="1" stop-color="white" stop-opacity="0"/>
          </linearGradient>
        </defs> -->
      </svg>
      <svg class="background-3" xmlns="http://www.w3.org/2000/svg"  viewBox="0 0 439 237" fill="none">
        <g :opacity="animalCard.opacity" filter="url(#filter0_f_1_592)">
          <ellipse class="background-3-color" cx="213" cy="273" rx="226" ry="95" :fill="animalCard.color"/>
        </g>
        <!-- <defs>
          <filter id="filter0_f_1_592" x="-191" y="0" width="808" height="546" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
            <feFlood flood-opacity="0" result="BackgroundImageFix"/>
            <feBlend mode="normal" in="SourceGraphic" in2="BackgroundImageFix" result="shape"/>
            <feGaussianBlur stdDeviation="89" result="effect1_foregroundBlur_1_592"/>
          </filter>
        </defs> -->
      </svg>
     <!-- <div class="background-4d" :style="{ background: 'url(' + animalCard.image + ')', width: animalCard.width, left: animalCard.left }"></div> -->
     <div class="bg"></div>
    </div>
    <div class="top-card">
      <div class="left-card-part">
        <div class="card-stick"></div>
        <h3 class="category-card">Tier</h3>
        <h1 class="premium-header">{{ animalCard.name }}</h1>
      </div>
      <img class="animal-img" :src="animalCard.image" :style="{left: animalCard.left, width: animalCard.width, height: animalCard.height}">
    </div>
  </div>
</template>

<script>
export default {
  name: "AnimalCard",
  props: {
    animalCard: Object
  }
}
</script>

<style lang="scss" scoped>
.item-card-object {
  position: relative;
  width: 21.9492rem;
  height: 19.296rem;
  border-radius: 1.35rem;
  border: 0.0495rem solid #1E1E1E;
  padding-top: 0.9495rem;
  padding-left: 1.7496rem;
  overflow: hidden;
  margin-bottom: 1.296rem;
  /*transition: transform 0.4s ease;*/
  .bg {
    position: absolute;
    width: 28.2375rem;
    height: 19.0125rem;
    background: linear-gradient(0deg, rgba(224,153,104,1) 0%, rgba(252,70,107,0) 40%);
    top: 0rem;
    opacity: 0.15;
    transition: .3s;
    left: -4.5rem;
  }

  &:hover {
      .animal-img {
        transform: scale(1.04);
      }
      .bg {
        background: linear-gradient(0deg, rgba(224,153,104,1) 0%, rgba(252,70,107,0) 60%);
        opacity: 0.2;
      }


  }
}

.active {
  border: 0.0495rem solid #535353;

}
.background {
  position: absolute;
  width: 100%;

&-1 {
  position: absolute;
  width: 21.9492rem;
  height: 19.296rem;
  top: -0.9rem;
  left: -1.998rem;
}

&-2 {
  position: absolute;
  width: 21.9492rem;
  height: 290.7rem;
  top: -133.2rem;
  left: -1.899rem;

}

&-3 {

  position: absolute;
  width: 21.9492rem;
  height: 11.8494rem;
  left: -1.7496rem;
  top: 6.543rem;
  display: none;
  //&-color {
  //  fill: #FC9B41;
  //}

}


}

.top-card {
  display: flex;
}

.left-card-part {
  margin-right: 1.899rem;
}

.card-stick {
  width: 1.296rem;
  height: 0.099rem;
  position: absolute;
  top: 1.6497rem;
  left: 0.297rem;
  background-color: #FFFFFF;
  border: 0.0495rem solid rgba(255, 255, 255, 0.09);
  box-shadow: 0px 0px 0.693rem rgba(255, 255, 255, 0.55);
  transform: rotate(-90deg);

}

.category-card {
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 700;
  font-size: 0.594rem;
  line-height: 0.693rem;
  text-transform: uppercase;

  color: rgba(255, 255, 255, 0.55);

}

.premium-header {
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 700;
  font-size: 2.394rem;
  line-height: 2.898rem;
  /* identical to box height */

  text-transform: uppercase;

  color: #FFFFFF;
  margin-top: 0.196rem;

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
;
}

.animal-img {
  position: absolute;
  // width: 13.3497rem;
  // height: 12.294rem;
  top: 4.1499rem;
  transition: .3s;
}
</style>