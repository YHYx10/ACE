<template>
    <div class="object-card">
        <div class="background">
            <div class="background-3"></div>
        </div>
        <div class="top-card">
            <div class="card-stick"></div>
            <h3 class="category-card">Category</h3>
            <h1 class="object-category" style="margin-bottom:-0.75rem">save</h1>
            <h1 class="object-category">Article</h1>
            <h3 class="subtitle-card">In this section you have the opportunity
             Buy clothes of different classes</h3>
        </div>
      <img class="object-img-main" src="/img/Shop/bag.png">
      <img class="object-img-mini" src="/img/Shop/gun.png">
    </div>
</template>

<script>
export default {
    name: "ObjectCard"
}
</script>

<style lang="scss" scoped>
* {
  box-sizing: border-box;
}
.object-card {
    width: 16.992rem;
    height: 18.792rem;
    border: 0.0563rem solid #171819;
    border-radius: 1.5188rem;
    position: relative;
    overflow: hidden;
    /*transition: transform 0.4s ease;

    &:hover {
        transform: scale(1.04);

    }*/
    &:hover {
        .background-3 {
            opacity: 0.25;
            transform: scale(1.5);
        }
        .object-img-main {
            transform: scale(1.02);
        }
        .object-img-mini {
            transform: scale(1.02);
        }
    }
}

.background {
    position: absolute;

    &-1 {
        position: absolute;
      width: 17.1rem;
      height: 21.7125rem;
        top: 0;
        left: 1.8rem;
    }

    &-2 {
        position: absolute;
      width: 19.125rem;
      height: 19.6313rem;
        top: 3.2062rem;
        left: 0;
    }

    &-3 {
        position: absolute;
      top: 10.8rem;
      left: 0rem;
        width: 18.7875rem;
        height: 19.6875rem;

        background: radial-gradient(circle, rgba(255,255,255,1) 0%, rgba(252,70,107,0) 70%);
        opacity: 0.15;
        transition: .3s;
        // filter: blur(5.0062rem);
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
    box-shadow: 0 0 0.7875rem rgba(255, 255, 255, 0.55);
    transform: rotate(-90deg);
    position: absolute;
    top: 2.25rem;
    left: 0;
}

.category-card {
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    font-size: 0.6rem;
    line-height: 0.7rem;
    text-transform: uppercase;

    color: rgba(255, 255, 255, 0.55);


}

.object-category {
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    font-size: 2.4rem;
    // line-height: 2.45rem;
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
.object-img {
  &-main {
    // filter: blur(0px);
    position: absolute;
    width: 5.85rem;
    height: 5.85rem;
    top: 13.05rem;
    left: 0.7312rem;
    transition: .3s;
  }
  &-mini {
    /*transform: matrix(-1, 0, 0, 1, 0, 0);*/
    position: absolute;
    width: 15.6492rem;
    height: 7.65rem;
    top: 8.325rem;
    left: 2.16rem;
    transition: .3s;
  }
}
</style>