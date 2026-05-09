<template>
  <div class="animal-description-card">
    <div class="card-stick"></div>
    <div class="animal-description-info">
      <h3 class="category-card"> The description of the animal</h3>
      <h1 class="premium-header">{{ animalsDescription.name }}</h1>
      <h3 class="subtitle-card">{{ animalsDescription.description }}</h3>
      <div class="pluses-part">
        <div class="pluse-card" v-for="item in animalsDescription.abilities" :key="item">
          <h3 class="pluse">+</h3>
          <h3 class="pluse-title">{{ item }}</h3>
        </div>
      </div>

      <div class="purchase-part">
        <button @click="buy(animalsDescription.name)" class="button-animal-buy-btn">
          <span class="buy-btn-text">Buy</span>
        </button>
        <div class="purchase-price">
          <div class="background">
            <div class="background-1"></div>
          </div>
          <div class="purchase-card-stick"></div>
          <img class="purchase-coin" src="/img/Shop/green-coin.png" alt="">
          
          <div class="purchase-text-part">
            <div class="valute">Coins.</div>
            <div class="count">{{animalsDescription.price}}</div>
          </div>
        </div>
      </div>

    </div>
  </div>
</template>

<script>
export default {
  name: "AnimalDescription",
  data() {
    return {
      pluses: ['knows how to bark', "asks for food, but little", "smart, but not very"]
    }
  },
  props: {
    animalsDescription: Object
  },
  methods: {
    buy(name){
      window.mp.triggerServer('server::pet:buyPet', name)
    }
  }
}
</script>

<style lang="scss" scoped>
.animal-description-card {
  position: relative;
  width: 32.7492rem;
  height: 19.35rem;

}

.background {
  position: absolute;
  &-1 {
    width: 8.397rem;
    height: 8.8497rem;
    left: 4.3497rem;
    top: 2.4498rem;

    background: radial-gradient(circle, rgba(255,255,255,1) 0%, rgba(252,70,107,0) 70%);
    opacity: 0.25;
    // filter: blur(4.45rem);
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
  top: 0.65rem;
  left: -0.65rem;

}
.animal-description-info {
  margin-left: 0.792rem;
  position: relative;
  height: 100%;
}
.category-card {
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 700;
  font-size: 0.5994rem;
  line-height: 0.6993rem;
  text-transform: uppercase;

  color: rgba(255, 255, 255, 0.55);
}
.premium-header {
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 700;
  font-size: 2.3994rem;
  line-height: 2.2995rem;
  /* identical to box height */
  margin-top: 0.296rem;
  text-transform: uppercase;

  color: #FFFFFF;
}
.subtitle-card {
  max-width: 31.95rem;
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 600;
  font-size: 0.75rem;
  line-height: 1.7496rem;
  text-transform: uppercase;
  margin-top: 1rem;
  margin-bottom: 0.45rem;
}
.pluses-part {
  display: flex;
  margin-bottom: 1.3993rem;
}
.pluse-card {
  display: flex;
  align-items: center;
  margin-right: 0.999rem;
}
.pluse {
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 900;
  font-size: 2.3994rem;
  line-height: 2.898rem;
  /* identical to box height */

  text-transform: uppercase;
  margin-right: 0.594rem;

  color: #5CFF80;

  text-shadow: 0px 0px 0.6993rem rgba(92, 255, 128, 0.25);

  &-title {
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    font-size: 0.7992rem;
    line-height: 0.9495rem;
    text-transform: uppercase;

    color: rgba(255, 255, 255, 0.85);

  }
}
.purchase-part {
  display: flex;
  position: absolute;
  bottom: 0.1rem;
}
.button-animal-buy-btn {

  display: flex;
  width: 17.694rem;
  height: 4.2408rem;
  background: linear-gradient(180deg, rgba(71, 44, 132, 0.25) 0%, rgba(75, 0, 130, 0.25) 100%);
  border: 0.0495rem solid #301934 ;
  box-shadow: inset 0px 0px 0.7497rem rgba(75, 0, 130, 0.86);
  justify-content: center;
  margin-right: 0.792rem;
  transition: 0.2s;
  &:hover {
      box-shadow: inset 0px 0px 6.1497rem rgba(75, 0, 130, 0.86);
  }
  //&:hover {
  //  background: #ccc;
  //}
  //&:hover .buy-btn-text{
  //  color: #301934 ;
  //}
}
.buy-btn-text {
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 600;
  font-size: 0.693rem;
  line-height: 0.8496rem;
  /* identical to box height */

  align-self: center;
  text-transform: uppercase;

  color: #FFFFFF;
}
.purchase-price {
  position: relative;
  width: 10.35rem;
  height: 4.2498rem;
  border: 0.0495rem solid #171819;
  overflow: hidden;
}
.purchase-card-stick {
  width: 1.296rem;
  height: 0.099rem;
  background: #FFFFFF;
  border: 0.0495rem solid rgba(255, 255, 255, 0.09);
  box-shadow: 0px 0px 0.693rem rgba(255, 255, 255, 0.55);
  transform: rotate(-90deg);
  position: absolute;
  top: 1.8rem;
  left: 0.0495rem;
}
.purchase-coin {
  position: absolute;
  width: 4.392rem;
  height: 4.392rem;
  left: 0.4995rem;
  top: 0;
  bottom: 0;
}
.purchase-text-part {
  margin-top: 1.1493rem;
  margin-left: 4.797rem;
}
.valute {
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 700;
  font-size: 0.693rem;
  line-height: 0.8496rem;
  /* identical to box height */

  text-transform: uppercase;

  color: rgba(255, 255, 255, 0.25);

}
.count {
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 700;
  font-size: 0.999rem;
  line-height: 1.197rem;
  /* identical to box height */

  text-transform: uppercase;

  color: #5CFF80;
}
</style>