<template>
  <div class="farm-house_shopbtn">
    <div class="farm-house_shopbtn-title">Save</div>
    <div class="farm-house_shopbtn-img">
      <img src="/img/farmHouse/store.png" alt="" />
      <img src="/img/farmHouse/store.png" alt="" />
    </div>
    <button
      class="farm-house_shopbtn-btn"
      :class="{
        'btn-hover': currentPage !== 'Shop',
        back: currentPage === 'Shop',
      }"
      @click="$emit('click-shop')"
    >
      <div>{{ currentPage === "Shop" ? "Close" : "OPEN" }}</div>
    </button>
  </div>
</template>

<script>
export default {
  props: {
    currentPage: String,
  },
};
</script>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.farm-house_shopbtn {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  background: linear-gradient(
    252.44deg,
    rgba(19, 20, 21, 0.99) 0%,
    rgba(31, 34, 37, 0.99) 100%
  );
  padding: conv(54) 0 conv(92);

  &-title {
    font-weight: 800;
    font-size: conv(40);
    line-height: conv(48);
    text-align: center;
    text-transform: uppercase;
    color: #1a1717;
    margin-bottom: conv(49);
    z-index: 5;
    position: relative;

    &.active {
      color: white;
    }
  }

  &-img {
    position: relative;
    width: conv(398);
    height: conv(398);

    img {
      &:first-child {
        width: 100%;
        height: 100%;
        z-index: 4;
        position: relative;
      }

      &:last-child {
        z-index: 3;
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(50%, 50%);
        width: conv(697);
        height: conv(697);
        opacity: 0.1;
      }
    }
  }

  &-btn {
    z-index: 5;
    position: relative;
    margin-top: conv(26);
    width: conv(295);
    height: conv(75);
    display: flex;
    justify-content: center;
    align-items: center;
    background: linear-gradient(180deg, #301934  0%, #591b87 100%, #591b87 100%);
    font-weight: 700;
    font-size: conv(24);
    line-height: conv(29);
    text-align: center;
    text-transform: uppercase;
    color: #ffffff;

    

    &.back {
      background: rgba(255, 255, 255, 0.1);

      &::after {
        content: "";
        transition: 0.2s ease;
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        z-index: 1;
        opacity: 0;
        background: rgba(255, 255, 255, 0.1);
      }

      div {
        z-index: 2;
      }

      &:hover {
        &::after {
          opacity: 1;
        }
      }
    }
  }
}

.btn-hover {
     border: 0.093vmin solid rgba($color: #301934 , $alpha: 1);
        background: linear-gradient(
          rgba($color: #301934 , $alpha: 0.25),
          rgba($color: #591b87, $alpha: 0.25)
        );
        transition: 0.3s ease;
        box-shadow: inset 0 0 1.389vmin rgba($color: #301934 , $alpha: 0.86);

        &:hover {
          transition: 0.3s ease;
          box-shadow: inset 0px 0px 7.5rem #301934 ;
          filter: drop-shadow(0px 0px conv(15) rgba(71, 44, 132, 0.5));
        }
    }
</style>