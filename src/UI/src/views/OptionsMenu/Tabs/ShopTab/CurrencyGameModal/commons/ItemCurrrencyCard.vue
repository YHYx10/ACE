<template>
  <div
    class="item-card-object"
    :id="objectCard.id + '-item'"
  >
    <div class="background">
      <div class="background-3"></div>
    </div>
    <div class="top-card">
      <div class="left-card-part">
        <div class="card-stick"></div>
        <h3 class="category-card">A number of play currency</h3>
        <h1 class="premium-header">{{ objectCard.name }}</h1>
        <!-- <h3 class="subtitle-card">
          In this section, you have the opportunity to buy clothes of various
class
        </h3> -->
      </div>
      <div class="right-card-part">
        <div class="right-card-text">
          <h3 class="subtitle-donate">Price</h3>
          <h2 class="count-premium">{{ objectCard.price }}</h2>
        </div>
        <img src="/img/Shop/green-coin.png" class="right-coin" alt="" />
      </div>
    </div>
    <img class="centre-img" :src="`/img/Shop/money-${objectCard.imgid}.png`" />
    <div class="bottom-card-modal">
      <button class="exclusive-button-buy" @click="buyMoney(objectCard)">
        <span class="exclusive-button-text">Buy</span>
      </button>
    </div>
  </div>
</template>

<script>
import {mapMutations} from 'vuex'
export default {
  name: 'ItemObjectCard',
  props: {
    objectCard: Object,
  },
  methods: {
    ...mapMutations('optionsMenu', ['setDialog']),
    modalClick() {
      this.$emit('modalClick', `${this.objectCard.id}-item`)
    },
    buyMoney: function (objectCard) {
      this.setDialog({
          input: undefined,
          callback: (val) => {
              window.mp.triggerServer("donate::buy", "money", objectCard.id);
              if (val) return;
          },
          tittle: 'Your account will be written off '+objectCard.price+'coins',
          subtittle: 'You really want to buy '+objectCard.name+' ?',
      });
    },
  },
  mounted(){
    console.log(this.objectCard);
  },
}
</script>

<style lang="scss" scoped>
.item-card-object {
  position: relative;
  width: 17.9rem;
  height: 19.692rem;
  border-radius: 1.35rem;
  border: 0.0502rem solid #1e1e1e;
  padding-top: 0.9495rem;
  overflow: hidden;

  /*transition: transform 0.4s ease;*/

  &:hover {
    .centre-img {
      transform: scale(1.03) translate(-50%);
    }
    .background-3 {
      opacity: 0.15;
      top: 1.8rem;
    }
  }
}

.active {
  .background .background-3 {
    opacity: 0.15;
    top: 1.8rem;
  }
}

.background {
  position: absolute;

  &-1 {
    position: absolute;
    width: 16.992rem;
    height: 19.296rem;
    top: -0.9rem;
    left: -1.692rem;
  }

  &-2 {
    position: absolute;
    width: 16.992rem;
    height: 17.4492rem;
    top: 1.8495rem;
    left: -1.899rem;
  }

  &-3 {
    position: absolute;
    width: 22.5rem;
    height: 18.495rem;
    opacity: 0.15;
    background: linear-gradient(0deg, rgba(255, 255, 255, 0.823) 0%, rgba(252,70,107,0) 92%);
    top: 10.8rem;
    left: -2.4498rem;
    transition: .3s;
  }

  &-4d {
    position: absolute;
    width: 8.793rem;
    height: 9.297rem;
    background: url('/img/Shop/shirt-object-modal.png');
    top: 6.696rem;
    left: 2.295rem;
  }
}

.top-card {
  display: flex;
  
  padding-left: 1.7496rem;
}

.left-card-part {
  margin-right: 1.899rem;
}

.card-stick {
  width: 1.296rem;
  height: 0.099rem;
  position: absolute;
  top: 2.0493rem;
  left: 0.396rem;
  background-color: #ffffff;
  border: 0.0495rem solid rgba(255, 255, 255, 0.09);
  box-shadow: 0rem 0rem 0.693rem rgba(255, 255, 255, 0.55);
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
  font-size: 1.197rem;
  line-height: 103%;
  /* or 2.315vw */

  text-transform: uppercase;

  color: #ffffff;
}

.subtitle-card {
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 700;
  font-size: 0.5994rem;
  line-height: 0.6993rem;
  text-transform: uppercase;
  margin-top: .5rem;
  color: rgba(255, 255, 255, 0.55);
  max-width: 10.197rem;
}

.right-card-part {
  padding-top: 1.197rem;
  display: flex;
  flex-direction: column;
}

.right-card-text {
  position: absolute;
  left: 11.1492rem;
  text-align: right;
  top: 0.999rem;
}

.subtitle-donate {
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 700;
  font-size: 0.5642rem;
  line-height: 0.693rem;
  /* identical to box height */

  text-align: right;
  text-transform: uppercase;

  color: rgba(255, 255, 255, 0.25);
}

.count-premium {
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 700;
  font-size: 0.999rem;
  line-height: 1.197rem;
  /* identical to box height */

  text-transform: uppercase;

  color: #5cff80;
}

.right-coin {
  position: absolute;
  width: 3.6rem;
  height: 3.6rem;
  top: 0.198rem;
  left: 13.7493rem;
}
.centre-img {
  position: absolute;
  z-index: 0;
  margin-top: 0.9rem;
  // width: 8.793rem;
  // height: 9.297rem;
  left: -1.7496rem;
  transition: 0.3s;
  // box-shadow: 0 0 0 0.0502rem red;
  left: 50%;
  transform: translate(-50%);
  // width: 70%;
  // height: auto;
}
.bottom-card-modal {
  position: absolute;
  bottom: 1.9003rem;
  left: 50%;
  transform: translate(-50%);
}

.exclusive-button-buy {
  z-index: 9999;
  width: 13.6501rem;
  height: 2.3501rem;
  cursor: pointer;
  // margin-right: 0.1998rem;
  background: linear-gradient(
    180deg,
    rgba(92, 255, 128, 0.25) 0%,
    rgba(17, 90, 33, 0.25) 100%
  );
  border: 0.0495rem solid #5cff80;
  transform: translate(0, 1rem);
  border: 1px solid #5CFF80;
  box-shadow: inset 0px 0px 15px rgb(92 255 128 / 86%);
  transition: 0.5s ease;
  &:hover {
    background: linear-gradient(180deg, rgba(92, 255, 128, 0.3) 0%, rgba(17, 90, 33, 0.3) 300%);
      box-shadow: inset 0px 0px 7.5rem #5CFF80;
      filter: drop-shadow(0px 0px 15px rgba(92, 255, 128, 0.3));
  }
}

.exclusive-button-text {
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 600;
  font-size: 0.6993rem;
  line-height: 0.8496rem;
  /* identical to box height */

  text-align: center;
  text-transform: uppercase;

  color: #ffffff;
}
</style>
