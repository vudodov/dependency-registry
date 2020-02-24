﻿using System;
 using System.Collections.Generic;
 using System.Linq;
 using DependencyRegistry.Tests.TestClasses;
 using FluentAssertions;
using Xunit;

namespace DependencyRegistry.Tests
{
    public class RegistryTests
    {
        [Fact]
        public void RegistryScansAssembly()
        {
            var registry = new Registry<TestDependee>();
            
            registry["test-dependee"].dependee.Should().Be(typeof(TestDependee));
            registry["test-dependee"].depender.Should().Be(typeof(TestDepender));
        }
        
        [Fact]
        public void RegistryWithComplicatedInheritance()
        {
            var registry = new Registry<IInheritanceTestDependee>();
            
            registry["inheritance-test-dependee-class-one"].dependee.Should().Be(typeof(InheritanceTestDependeeClassOne));
            registry["inheritance-test-dependee-class-one"].depender.Should().Be(typeof(InheritanceTestDependerClassOne));
            
            registry["inheritance-test-dependee-class-two"].dependee.Should().Be(typeof(InheritanceTestDependeeClassTwo));
            registry["inheritance-test-dependee-class-two"].depender.Should().Be(typeof(InheritanceTestDependerClassTwo));

            registry.Count().Should().Be(2);
        }
        
        [Fact]
        public void ShouldThrowIfDoesNotExist()
        {
            var registry = new Registry<TestDependee>();

            Func<Type> func = () => registry["does-not-exist-dependee"].dependee;
            func.Should().Throw<KeyNotFoundException>();
        }
        
        [Fact]
        public void ShouldThrowIfTooManyDpenders()
        {
            Action action = () => new Registry<TooManyDependersDependee>();
            action.Should().Throw<InvalidOperationException>();
        }
        
        [Fact]
        public void ShouldThrowIfNoDpenders()
        {
            Action action = () => new Registry<NoDependersDependee>();
            action.Should().Throw<InvalidOperationException>();
        }
    }
}