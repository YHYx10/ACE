<template>
  <div class="item">
    <div class="nickname online">{{ item.nickname }}</div>
    <div class="rang">{{ item.rang }}</div>
    <div class="btn">
      <img
        src="/img/captures/x.svg"
        alt=""
        @click="kickMember(item.nickname)"
      />
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  name: "TeamListItem",

  computed: {
    ...mapState("captures", ["capturing", "gangList", "teamList"]),
  },

  props: {
    item: Object,
    index: Number,
  },

  methods: {
    kickMember: function (value) {
      window.mp.trigger("capt:kickMember", value);
    },
  },
};
</script>

<style lang="scss" scoped>
$width: 1920;
$height: 1080;

@function conv($px, $direction: false) {
  @if $direction {
    @return ($px / $height) * 100vh;
  } @else {
    @return ($px / $width) * 100vw;
  }
}

.item {
  overflow: hidden;
  position: relative;
  height: conv(72, true);
  min-height: conv(72, true);
  width: 100%;
  display: grid;
  grid-template-columns: conv(198) conv(150) 1fr;
  column-gap: conv(45);
  padding: 0 conv(70) 0 conv(40);
  border: 1px solid rgba(255, 255, 255, 0.09);
  background: rgba(255, 255, 255, 0.05);

  &:not(:last-child) {
    margin-bottom: conv(3, true);
  }

  &::after {
    content: "";
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: linear-gradient(
      90deg,
      rgba(92, 255, 128, 0.05) 0%,
      rgba(92, 255, 128, 0) 100%
    );
    z-index: 5;
    opacity: 1;
    transition: 0.2s  ease;
  }

  div,
  span,
  img,
  button {
    z-index: 10;
  }

  /* &::after,
  &::before {
    content: "";
    position: absolute;
    bottom: conv(-59, true);
    left: 50%;
    transform: translate(-50%, 100%);
    width: 100%;
    height: conv(52, true);
    filter: blur(89px);
    border-radius: 50%;
    z-index: 5;
    transition: opacity 0.3s linear;
  }

  &::after {
    background: rgba(255, 255, 255, 0.55);
    opacity: 0;
  }

  &::before {
    background: rgba(92, 255, 128, 0.55);
    opacity: 1;
  } */

  &:hover {
    &::after {
      opacity: 0;
      transition: 0.2s  ease;
    }
    /* &::after {
      opacity: 1;
    }

    &::before {
      opacity: 0;
    } */

    .btn {
      img {
        display: flex;
      }
    }
  }

  div,
  button,
  span,
  img {
    z-index: 6;
  }

  .rang,
  .nickname {
    display: flex;
    align-items: center;
    font-weight: 700;
    font-size: conv(18, true);
    line-height: conv(22, true);
    text-transform: uppercase;
    color: #ffffff;
  }

  .nickname {
    position: relative;
    width: conv(198); //44%;
    /* margin-right: conv(45); */

    &:before {
      content: "";
      position: absolute;
      top: 50%;
      left: conv(-13);
      transform: translate(-100%, -50%);
      width: conv(8, true);
      height: conv(8, true);
      border-radius: 50%;
      background: #5cff80;
    }
  }

  .rang {
    width: conv(150); //37%;
  }

  .btn {
    width: 100%;
    height: 100%;
    display: flex;
    align-items: center;
    justify-content: flex-end;
  }

  .btn {
    img {
      display: none;
      width: auto;
      height: conv(16.33, true);
    }
  }
}
</style>