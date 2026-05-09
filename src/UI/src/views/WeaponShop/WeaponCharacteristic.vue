<template>
  <div class="weapon-characteristic">
    <div class="title">
    Waffen Eigenschaften:
    </div>
    <div class="list">
      <div class="about">
        <div class="item">
          <AmmoSvg class="ammo" :fill="'#fff'" :fillOpacity="'1'" />
          <div class="value">{{ ammoType }}<span>mm</span></div>
        </div>
        <div class="item">
          <WeightSvg class="weight" :fill="'#fff'" />
          <div class="value">{{ weight }}<span>kg</span></div>
        </div>
        <div class="item">
          <MagazineSVG class="magazine" />
          <div class="value">{{ maxAmmo }}<span>pcs</span></div>
        </div>
      </div>

      <div class="progress-bars">
        <div class="progress-item">
          <div class="type">
            Schussrate
          </div>
          <SegmentProgressBar
            :amount="7"
            :value="Number(
              shootingCharacteristics.FireRate
            )"
          />
        </div>
        <div class="progress-item">
          <div class="type">
            Damage
          </div>
          <SegmentProgressBar
            :amount="7"
            :value="Number(
              shootingCharacteristics.Damage
            )"
          />
        </div>
        <div class="progress-item">
          <div class="type">
            Accuracy
          </div>
          <SegmentProgressBar
            :amount="7"
            :value="Number(
              shootingCharacteristics.Accuracy
            )"
          />
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import SegmentProgressBar from '../UI/components/SegmentProgressBar.vue'
import AmmoSvg from './componentsSVG/ammoSvg.vue'
import WeightSvg from './componentsSVG/weightSvg.vue'
import MagazineSVG from './componentsSVG/magazineSVG.vue'
export default {
  props: {
    ammoType: String,
    weight: Number,
    maxAmmo: Number,
    img: String,
    shootingCharacteristics: Object
  },
  components: { SegmentProgressBar, AmmoSvg, WeightSvg, MagazineSVG },
}
</script>

<style lang="scss" scoped>
.weapon-characteristic {
  margin-top: 0.28vh;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  .title {
    font-family: 'Montserrat';
    font-style: normal;
    font-weight: 700;
    font-size: 1.85vh;
    line-height: 2.22vh;
    color: #ffffff;
  }
  .list {
    margin-top: 4.61vh;
    display: flex;
    align-items: center;
    justify-content: space-between;
    .about {
      width: 44.07vh;
      color: #ffffff;
      display: flex;
      gap: 3.89vh;
      .item {
        display: flex;
        align-items: center;
        gap: 1.02vh;
        .ammo,
        .weight {
          width: 3.24vh;
        }
        .magazine {
          width: 2.31vh;
        }
        .value {
          font-family: 'Montserrat';
          font-style: normal;
          font-weight: 700;
          font-size: 1.85vh;
          line-height: 2.22vh;
          color: #e8e8e8;
          span {
            margin-left: 0.56vh;
            font-weight: 400;
            color: rgba(255, 255, 255, 0.24);
          }
        }
      }
      @media screen and (max-aspect-ratio: 4/3) {
        width: auto;
        .item .ammo,
        .item .weight {
          width: 2.24vh;
        }
        .item .magazine {
          width: 1.8vh;
        }
      }
    }

    .progress-bars {
      display: flex;
      gap: 4.815vh;
      .progress-item {
        width: 26.667vh;
        display: flex;
        flex-direction: column;
        gap: 1.296vh;
        .type {
          font-family: 'Montserrat';
          font-style: normal;
          font-weight: 500;
          font-size: 1.48vh;
          line-height: 1.85vh;
          color: #ffffff;
        }
      }
      @media screen and (max-aspect-ratio: 4/3) {
        gap: 2.815vh;
        .progress-item {
          width: 15.667vh;
        }
      }
    }
  }
}
</style>
