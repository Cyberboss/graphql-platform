import React, { FC } from "react";
import styled from "styled-components";

import { SiteLayout } from "@/components/layout";
import {
  ContentSection,
  Hero,
  HeroLink,
  HeroTeaser,
  HeroTitleFirst,
  HeroTitleSecond,
  NextStepsContentSection,
  SEO,
} from "@/components/misc";
import {
  CommunitySection,
  CompaniesSection,
  DeploymentOptionsSection,
  MostRecentBlogPostsSection,
  NewsletterSection,
} from "@/components/widgets";
import { useAnimationIntersectionObserver } from "@/hooks";
import { FADE_IN, ZOOM_IN } from "@/style";

// Images
import FusionSvg from "@/images/startpage/fusion.svg";
import {
  COLLABORATE_IMAGE_WIDTH,
  CollaborateImage,
  EVOLVE_IMAGE_WIDTH,
  EvolveImage,
  MOVE_FASTER_IMAGE_WIDTH,
  MoveFasterImage,
  OBSERVE_IMAGE_WIDTH,
  ObserveImage,
  SHIP_WITH_CONFIDENCE_IMAGE_WIDTH,
  ShipWithConfidenceImage,
} from "./images";

const IndexPage: FC = () => {
  useAnimationIntersectionObserver();

  return (
    <SiteLayout>
      <SEO title="Home" />
      <BackgroundContainer>
        <Fusion />
      </BackgroundContainer>
      <IndexPageHero>
        <HeroTitleFirst>Unleash the Power</HeroTitleFirst>
        <HeroTitleSecond>of Unified Services</HeroTitleSecond>
        <HeroTeaser>
          Unify all your APIs into a comprehensive company graph, streamlining
          data accessibility and enhancing integration. Transform the way you
          manage and interact with your data.
        </HeroTeaser>
        <HeroLink to="/docs/bananacakepop/v2/apis/fusion">Get Started</HeroLink>
      </IndexPageHero>
      <CompaniesSection />
      <ContentSection
        title="Move Faster"
        text="
          Minimize the effort required to integrate, orchestrate, and compose APIs, enabling teams
          to collaborate seamlessly. By introducing a unified and composable platform, backend teams
          can avoid building isolated backends-for-frontend or single purpose APIs. Instead, they
          contribute to a cohesive system where the domain and business rules are defined once, even
          if distributed across different teams or services. This approach ensures consistency and
          reduces the workload, allowing applications to come to life faster and more efficiently.
        "
        image={<MoveFasterImage />}
        imageWidth={MOVE_FASTER_IMAGE_WIDTH}
      />
      <ContentSection
        title="Ship With Confidence"
        text="
          Deploy APIs with confidence, knowing each update integrates 
          flawlessly. Ensure that every change is safe, allowing you to ship 
          your systems without disruption. You can deploy without breaking your 
          api or causing downtime, and understand the impact of each change 
          before it happens. Leverage the precision of GraphQL's strong 
          typing for deployments you can trust, and gain insights into which 
          features are being used to make informed decisions.
        "
        image={<ShipWithConfidenceImage />}
        imageWidth={SHIP_WITH_CONFIDENCE_IMAGE_WIDTH}
      >
        <p className="text-2"></p>
      </ContentSection>
      <ContentSection
        title="Observe"
        text="
          Unlock real-time GraphQL insights and elevate your system's 
          performance. Gain a unified view of your API data, see the health of
          your entire application, and understand the impact of client load. 
          Delve deep into traces with detailed error analysis and latency 
          insights. 
        "
        image={<ObserveImage />}
        imageWidth={OBSERVE_IMAGE_WIDTH}
      />
      <ContentSection
        title="Collaborate"
        text="
          A central hub for team-based API management, designed to bring 
          together request documents, testing, authorization settings, and more. 
          Manage APIs, gateways, and testing from a single point to enhance team 
          alignment and efficiency.
        "
        image={<CollaborateImage />}
        imageWidth={COLLABORATE_IMAGE_WIDTH}
      />
      {false && (
        <ContentSection
          title="Evolve"
          text="
            In a world where change is the only constant, this solution empowers
            teams to adapt APIs with ease. Propose and test API changes
            collaboratively, providing immediate mocking for a smooth, unified
            development process.
          "
          image={<EvolveImage />}
          imageWidth={EVOLVE_IMAGE_WIDTH}
        />
      )}
      <DeploymentOptionsSection />
      <NextStepsContentSection
        title="Discover the Difference"
        text="
          Ready to explore how our platform can transform your API management?
          Book a demo to see it in action, or dive right in and start now.
          We're here to help you revolutionize your API strategy.
        "
        primaryLink="mailto:contact@chillicream.com?subject=Demo"
        primaryLinkText="Book a Demo"
        secondaryLink="https://eat.bananacakepop.com"
        secondaryLinkText="Launch"
      />
      <CommunitySection />
      <NewsletterSection />
      <MostRecentBlogPostsSection />
    </SiteLayout>
  );
};

export default IndexPage;

const Fusion = styled(FusionSvg)`
  animation: 0.5s ease-in-out ${FADE_IN} forwards,
    0.5s ease-in-out ${ZOOM_IN} forwards;
  opacity: 0;
  transform: translateZ(-1px);
`;

const BackgroundContainer = styled.div`
  position: absolute;
  z-index: -1;
  top: 480px;
  display: flex;
  align-items: center;
  perspective: 1px;

  > ${Fusion} {
    width: 120vw;
    height: unset;
  }

  @media only screen and (min-width: 304px) {
    top: 460px;
  }

  @media only screen and (min-width: 326px) {
    top: 400px;
  }

  @media only screen and (min-width: 346px) {
    top: 380px;
  }

  @media only screen and (min-width: 383px) {
    top: 340px;
  }

  @media only screen and (min-width: 396px) {
    top: 290px;
  }

  @media only screen and (min-width: 447px) {
    top: 240px;
  }

  @media only screen and (min-width: 500px) {
    top: 200px;
  }

  @media only screen and (min-width: 550px) {
    top: 180px;
  }

  @media only screen and (min-width: 565px) {
    top: 160px;
  }

  @media only screen and (min-width: 600px) {
    top: 100px;
  }

  @media only screen and (min-width: 700px) {
    top: 80px;
  }

  @media only screen and (min-width: 746px) {
    top: 40px;
  }

  @media only screen and (min-width: 826px) {
    top: 20px;
  }

  @media only screen and (min-width: 835px) {
    top: 0px;
  }

  @media only screen and (min-width: 835px) {
    top: 0px;
  }

  @media only screen and (min-width: 870px) {
    top: -20px;
  }

  @media only screen and (min-width: 900px) {
    top: -40px;
  }

  @media only screen and (min-width: 930px) {
    top: -60px;
  }

  @media only screen and (min-width: 980px) {
    top: -80px;
  }

  @media only screen and (min-width: 992px) {
    top: 216px;
    max-width: 1246px;

    > ${Fusion} {
      width: 1246px;
      height: unset;
    }
  }
`;

const IndexPageHero = styled(Hero)`
  padding-bottom: 180px;

  @media only screen and (min-width: 992px) {
    padding-bottom: 380px;
  }
`;
